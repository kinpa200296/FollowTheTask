using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Identity;
using FollowTheTask.Models.DataBase;
using FollowTheTask.Models.Managers;
using FollowTheTask.Models.TrackedTasks;
using Microsoft.AspNet.Identity.Owin;
using QuestModel = FollowTheTask.Models.Quests.QuestModel;

namespace FollowTheTask.Controllers
{
    public class ManagersController : Controller
    {
        private ApplicationContext AppContext
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationContext>(); }
        }

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        [HttpGet]
        public ActionResult Index()
        {
            var managers = AppContext.Managers.Include(m => m.User).ToList();
            var trackedTasks = AppContext.TrackedTasks.ToLookup(trackedTask => trackedTask.ManagerId);
            var workers = AppContext.Workers.Include(w => w.User).ToLookup(worker => worker.ManagerId);
            foreach (var manager in managers)
            {
                manager.TrackedTasks = trackedTasks.Contains(manager.Id)
                    ? trackedTasks.First(t => t.Key == manager.Id).AsEnumerable()
                    : null;
                manager.Workers = workers.Contains(manager.Id)
                    ? workers.First(w => w.Key == manager.Id).AsEnumerable()
                    : null;
            }
            var model = managers.Select(x => new ManagerModel(x)).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не существует";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является менеджером";
                return View("Error");
            }
            user.Manager = AppContext.Managers.Include(m => m.User).First(x => x.Id == user.ManagerId);
            user.Manager.TrackedTasks = AppContext.TrackedTasks.Where(t => t.ManagerId == user.ManagerId);
            user.Manager.Workers = AppContext.Workers.Include(w => w.User).Where(w => w.ManagerId == user.ManagerId);
            var manager = new ManagerModel(user.Manager);
            return View(manager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> WorkerApply(string username)
        {
            var manager = await UserManager.FindByNameAsync(username);
            if (manager == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не существует";
                return View("Error");
            }
            if (manager.ManagerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является менеджером";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user.WorkerId == null)
            {
                ViewBag.ErrorMessage = "Вы не являетесь исполнителем";
                return PartialView(false);
            }
            if (!await UserManager.IsInRoleAsync(user.Id, "worker"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return PartialView(false);
            }
            user.Worker = AppContext.Workers.Find(user.WorkerId);
            if (user.Worker.ManagerId != null)
            {
                ViewBag.ErrorMessage = "У вас уже есть менеджер";
                return PartialView(false);
            }
            var token = await UserManager.GenerateUserTokenAsync("ApplyToManager", manager.Id);
            var callbackUrl = Utility.GetCallbackUrl(Url, "ConfirmWorker", "Managers",
                new { username = username, workerId = user.Id, token = token },
                Request.Url.Scheme, Request.Url.Host);
            var workerUrl = Utility.GetCallbackUrl(Url, "Details", "Workers", new {username = user.UserName},
                Request.Url.Scheme, Request.Url.Host);
            await
                UserManager.SendEmailAsync(manager.Id, "Подтверждение исполнителя",
                    "Исполнитель <a href=\""+ workerUrl +"\">" + user.UserName + "</a>" +
                    " хочет работать с Вами. Подтвердите исполнителя, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");
            ViewBag.Message =
                "Менеджеру было выслано оповещение. Как только он подтвердит ваше участие мы вышлем вам уведомление";
            return PartialView(true);
        }

        [HttpGet]
        public async Task<ActionResult> ConfirmWorker(string username, string workerId, string token)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var manager = await UserManager.FindByNameAsync(username);
            var user = await UserManager.FindByIdAsync(workerId);
            user.Worker = AppContext.Workers.Find(user.WorkerId);
            if (user.Worker.ManagerId != null)
            {
                ViewBag.ErrorMessage = "У пользователя уже есть менеджер";
                return View(false);
            }
            var result = await UserManager.VerifyUserTokenAsync(workerId, "ApplyToManager", token);
            if (!result)
            {
                ViewBag.ErrorMessage = "Ключ подтверждения недействителен.";
                return View(false);
            }
            user.Worker.ManagerId = manager.ManagerId;
            AppContext.Entry(user.Worker).State = EntityState.Modified;
            AppContext.SaveChanges();
            var managerUrl = Utility.GetCallbackUrl(Url, "Details", "Managers", new { username = manager.UserName },
                Request.Url.Scheme, Request.Url.Host);
            await
                UserManager.SendEmailAsync(user.Id, "Исполнитель подтвержден",
                    "Менеджер <a href=\"" + managerUrl + "\">" + manager.UserName + "</a> подтвердил ваше участие.");
            ViewBag.Message = "Теперь вы можете выдавать подзадания пользователю " + user.UserName;
            return View(true);
        }

        [HttpGet]
        public async Task<ActionResult> TasksComplete(string username, int id)
        {
            if (User.Identity.Name != username)
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не существует";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является менеджером";
                return View("Error");
            }
            var task = AppContext.TrackedTasks.Find(id);
            if (task == null || task.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Запрошенная задача не найдена";
                return View("Error");
            }
            if (task.IsFinished)
            {
                ViewBag.ErrorMessage = "Задача уже сдана";
                return PartialView();
            }
            task.Manager = AppContext.Managers.Find(task.ManagerId);
            task.Quests = AppContext.Quests.Include(q => q.Worker).Where(q => q.TrackedTaskId == task.Id);
            if (task.Quests.Any(q => !q.IsFinished))
                {
                    ViewBag.ErrorMessage = "Не все подзадачи сданы";
                    return PartialView();
                }
            var model = new TrackedTaskModel(task);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TasksComplete(string username, int id,
            [Bind(Include = "Title,Description,HoursSpent")] TrackedTaskModel model)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.Name != username)
                {
                    ViewBag.ErrorMessage = "Недостаточно прав";
                    return View("Error");
                }
                var user = await UserManager.FindByNameAsync(username);
                if (user == null)
                {
                    ViewBag.ErrorMessage = "Данный пользователь не существует";
                    return View("Error");
                }
                if (user.ManagerId == null)
                {
                    ViewBag.ErrorMessage = "Данный пользователь не является менеджером";
                    return View("Error");
                }
                var task = AppContext.TrackedTasks.Find(id);
                if (task == null || task.ManagerId != user.ManagerId)
                {
                    ViewBag.ErrorMessage = "Запрошенная задача не найдена";
                    return View("Error");
                }
                if (task.IsFinished)
                {
                    ViewBag.ErrorMessage = "Задача уже сдана";
                    return PartialView();
                }
                task.Manager = AppContext.Managers.Find(task.ManagerId);
                task.Quests = AppContext.Quests.Include(q => q.Worker).Where(q => q.TrackedTaskId == task.Id);
                if (task.Quests.Any(q => !q.IsFinished))
                {
                    ViewBag.ErrorMessage = "Не все подзадачи сданы";
                    return PartialView();
                }
                var newModel = new TrackedTaskModel(task) { HoursSpent = model.HoursSpent };
                if (model.HoursSpent < 1)
                {
                    ModelState.AddModelError("HoursSpent", "Так мало работать невозможно");
                    return PartialView(newModel);
                }
                var span = DateTime.Now - task.IssuedDate;
                if (model.HoursSpent > span.TotalHours)
                {
                    ModelState.AddModelError("HoursSpent", "С момента создания задачи не прошло столько часов");
                    return PartialView(newModel);
                }
                task.IsFinished = true;
                task.CompletionDate = DateTime.Now;
                task.HoursSpent = model.HoursSpent;
                AppContext.Entry(task).State = EntityState.Modified;
                AppContext.SaveChanges();
                ViewBag.Message = "Задача сдана. Обновите страницу";
                model = new TrackedTaskModel(task);
                return PartialView(model);
            }
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user.ManagerId != null)
            {
                return PartialView(true);
            }
            var manager = new Manager {UserId = user.Id, User = user};
            manager = AppContext.Managers.Add(manager);
            AppContext.SaveChanges();
            user.ManagerId = manager.Id;
            var result = await UserManager.UpdateAsync(user);
            return PartialView(result.Succeeded);
        }

        [HttpGet]
        public async Task<ActionResult> TasksIndex(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не существует";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является менеджером";
                return View("Error");
            }
            var trackedTasks =
                AppContext.TrackedTasks.Include(t => t.Manager).Where(t => t.ManagerId == user.ManagerId).ToList();
            var quests = AppContext.Quests.ToLookup(q => q.TrackedTaskId);
            foreach (var task in trackedTasks)
            {
                task.Quests = quests.Contains(task.Id) ? quests.First(q => q.Key == task.Id).AsEnumerable() : null;
            }
            var model = trackedTasks.Select(x => new TrackedTaskModel(x));
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> TasksDetails(string username, int id)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не существует";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является менеджером";
                return View("Error");
            }
            var trackedTask = AppContext.TrackedTasks.Find(id);
            if (trackedTask == null || trackedTask.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Запрошенная задача не найдена";
                return View("Error");
            }
            trackedTask.Manager = AppContext.Managers.Find(trackedTask.ManagerId);
            trackedTask.Quests =
                AppContext.Quests.Include(q => q.Worker).Include(q => q.Worker.User)
                    .Where(q => q.TrackedTaskId == trackedTask.Id);
            var model = new TrackedTaskModel(trackedTask);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> TasksCreate(string username)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                return RedirectToAction("Index", "Managers");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TasksCreate(string username,
            [Bind(Include = "Title,Description,IssuedDate,DeadLine")] TrackedTaskModel model)
        {
            if (ModelState.IsValid)
            {
                if (username != User.Identity.Name)
                {
                    ViewBag.ErrorMessage = "Доступ запрещен";
                    return View("Error");
                }
                var user = await UserManager.FindByNameAsync(username);
                if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
                {
                    ViewBag.ErrorMessage = "Недостаточно прав";
                    return View("Error");
                }
                if (user.ManagerId == null)
                {
                    return RedirectToAction("Index", "Managers");
                }
                if (model.IssuedDate >= model.DeadLine)
                {
                    ModelState.AddModelError("DeadLine", "Крайний срок не может быть раньше(одновременно) даты выдачи");
                    return View(model);
                }
                var trackedTask = new TrackedTask
                {
                    Title = model.Title,
                    Description = model.Description,
                    IssuedDate = model.IssuedDate,
                    DeadLine = model.DeadLine,
                    ManagerId = (int)user.ManagerId,
                    IsFinished = false
                };
                AppContext.TrackedTasks.Add(trackedTask);
                AppContext.SaveChanges();
                return RedirectToAction("TasksIndex", "Managers", new { username = username });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> TasksEdit(string username, int id)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                return RedirectToAction("Index", "Managers");
            }
            var trackedTask = AppContext.TrackedTasks.Find(id);
            if (trackedTask == null || trackedTask.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Запрошенная задача не найдена";
                return View("Error");
            }
            trackedTask.Manager = AppContext.Managers.Find(trackedTask.ManagerId);
            var model = new TrackedTaskModel(trackedTask);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TasksEdit(string username,
            [Bind(Include = "Id,Title,Description,DeadLine")] TrackedTaskModel model)
        {
            if (ModelState.IsValid)
            {
                if (username != User.Identity.Name)
                {
                    ViewBag.ErrorMessage = "Доступ запрещен";
                    return View("Error");
                }
                var user = await UserManager.FindByNameAsync(username);
                if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
                {
                    ViewBag.ErrorMessage = "Недостаточно прав";
                    return View("Error");
                }
                if (user.ManagerId == null)
                {
                    return RedirectToAction("Index", "Managers");
                }
                var trackedTask = AppContext.TrackedTasks.Find(model.Id);
                if (trackedTask == null || trackedTask.ManagerId != user.ManagerId)
                {
                    ViewBag.ErrorMessage = "Запрошенная задача не найдена";
                    return View("Error");
                }
                if (trackedTask.IssuedDate >= model.DeadLine)
                {
                    ModelState.AddModelError("DeadLine", "Крайний срок не может быть раньше(одновременно) даты выдачи");
                    trackedTask.Manager = AppContext.Managers.Find(trackedTask.ManagerId);
                    model = new TrackedTaskModel(trackedTask);
                    return View(model);
                }
                trackedTask.Title = model.Title;
                trackedTask.Description = model.Description;
                if (!trackedTask.IsFinished)
                {
                    trackedTask.DeadLine = model.DeadLine;
                }
                AppContext.Entry(trackedTask).State = EntityState.Modified;
                AppContext.SaveChanges();
                return RedirectToAction("TasksDetails", "Managers", new { username = username, id = model.Id });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> TasksDelete(string username, int id)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return PartialView();
            }
            var user = await UserManager.FindByNameAsync(username);
            if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return PartialView();
            }
            if (user.ManagerId == null)
            {
                return RedirectToAction("Index", "Managers");
            }
            var trackedTask = AppContext.TrackedTasks.Find(id);
            if (trackedTask == null || trackedTask.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Запрошенная задача не найдена";
                return PartialView();
            }
            trackedTask.Manager = AppContext.Managers.Find(trackedTask.ManagerId);
            var model = new TrackedTaskModel(trackedTask);
            return PartialView(model);
        }

        [HttpPost, ActionName("TasksDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TasksDeleteConfirmed(string username, int id)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                return RedirectToAction("Index", "Managers");
            }
            var trackedTask = AppContext.TrackedTasks.Find(id);
            if (trackedTask == null || trackedTask.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Запрошенная задача не найдена";
                return PartialView();
            }
            AppContext.TrackedTasks.Remove(trackedTask);
            AppContext.SaveChanges();
            var model = new TrackedTaskModel(trackedTask) {Id = -1};
            return PartialView(model);
        }

        [HttpGet]
        public async Task<ActionResult> QuestsDetails(string username, int taskId, int id)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не существует";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является менеджером";
                return View("Error");
            }
            var quest = AppContext.Quests.Find(id);
            if (quest == null || quest.TrackedTaskId != taskId)
            {
                ViewBag.ErrorMessage = "Запрошенная подзадача не найдена";
                return View("Error");
            }
            quest.Worker = AppContext.Workers.Find(quest.WorkerId);
            quest.Worker.User = await UserManager.FindByIdAsync(quest.Worker.UserId);
            quest.TrackedTask = AppContext.TrackedTasks.Find(quest.TrackedTaskId);
            if (quest.TrackedTask.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Запрошенная подзадача не найдена";
                return View("Error");
            }
            ViewBag.UserName = username;
            var model = new QuestModel(quest);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> QuestsCreate(string username, int taskId)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                return RedirectToAction("Index", "Managers");
            }
            var trackedTask = AppContext.TrackedTasks.Find(taskId);
            if (trackedTask == null)
            {
                ViewBag.ErrorMessage = "Запрошенная задача не найдена";
                return View("Error");
            }
            if (trackedTask.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            if (trackedTask.IsFinished)
            {
                ViewBag.ErrorMessage = "Выполнение задачи завершено";
                return View("Error");
            }
            ViewBag.TaskId = taskId;
            var workerNames =
                AppContext.Workers.Include(w => w.User)
                    .Where(w => w.ManagerId == user.ManagerId)
                    .Select(w => w.User.UserName).ToList();
            ViewBag.workerName = new SelectList(workerNames);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> QuestsCreate(string username, int taskId, string workerName,
            [Bind(Include = "Title,Description,DeadLine")] QuestModel model)
        {
            if (ModelState.IsValid)
            {
                if (username != User.Identity.Name)
                {
                    ViewBag.ErrorMessage = "Доступ запрещен";
                    return View("Error");
                }
                var user = await UserManager.FindByNameAsync(username);
                if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
                {
                    ViewBag.ErrorMessage = "Недостаточно прав";
                    return View("Error");
                }
                if (user.ManagerId == null)
                {
                    return RedirectToAction("Index", "Managers");
                }
                var trackedTask = AppContext.TrackedTasks.Find(taskId);
                if (trackedTask == null)
                {
                    ViewBag.ErrorMessage = "Запрошенная задача не найдена";
                    return View("Error");
                }
                if (trackedTask.ManagerId != user.ManagerId)
                {
                    ViewBag.ErrorMessage = "Недостаточно прав";
                    return View("Error");
                }
                if (DateTime.Now >= model.DeadLine)
                {
                    ModelState.AddModelError("DeadLine", "Крайний срок не может быть раньше, чем завтра");
                    ViewBag.TaskId = taskId;
                    var workerNames =
                        AppContext.Workers.Include(w => w.User)
                            .Where(w => w.ManagerId == user.ManagerId)
                            .Select(w => w.User.UserName).ToList();
                    ViewBag.workerName = new SelectList(workerNames);
                    return View(model);
                }
                var worker = await UserManager.FindByNameAsync(workerName);
                if (worker == null || worker.WorkerId == null)
                {
                    ModelState.AddModelError("", "Выбранного исполнителя не существует");
                    ViewBag.TaskId = taskId;
                    var workerNames =
                        AppContext.Workers.Include(w => w.User)
                            .Where(w => w.ManagerId == user.ManagerId)
                            .Select(w => w.User.UserName).ToList();
                    ViewBag.workerName = new SelectList(workerNames);
                    return View(model);
                }
                var quest = new Quest
                {
                    Title = model.Title,
                    Description = model.Description,
                    IssuedDate = DateTime.Now,
                    DeadLine = model.DeadLine,
                    TrackedTaskId = taskId,
                    WorkerId = (int) worker.WorkerId,
                    IsFinished = false
                };
                AppContext.Quests.Add(quest);
                AppContext.SaveChanges();
                return RedirectToAction("TasksDetails", "Managers", new { username = username, id = taskId});
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> QuestsEdit(string username, int taskId, int id)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                return RedirectToAction("Index", "Managers");
            }
            var quest = AppContext.Quests.Find(id);
            if (quest == null || quest.TrackedTaskId != taskId)
            {
                ViewBag.ErrorMessage = "Запрошенная подзадача не найдена";
                return View("Error");
            }
            quest.TrackedTask = AppContext.TrackedTasks.Find(taskId);
            if (quest.TrackedTask.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            quest.Worker = AppContext.Workers.Find(quest.WorkerId);
            var model = new QuestModel(quest);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> QuestsEdit(string username, int taskId,
            [Bind(Include = "Id,Title,Description,DeadLine")] QuestModel model)
        {
            if (ModelState.IsValid)
            {
                if (username != User.Identity.Name)
                {
                    ViewBag.ErrorMessage = "Доступ запрещен";
                    return View("Error");
                }
                var user = await UserManager.FindByNameAsync(username);
                if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
                {
                    ViewBag.ErrorMessage = "Недостаточно прав";
                    return View("Error");
                }
                if (user.ManagerId == null)
                {
                    return RedirectToAction("Index", "Managers");
                }
                var quest = AppContext.Quests.Find(model.Id);
                if (quest == null || quest.TrackedTaskId != taskId)
                {
                    ViewBag.ErrorMessage = "Запрошенная подзадача не найдена";
                    return View("Error");
                }
                quest.TrackedTask = AppContext.TrackedTasks.Find(taskId);
                if (quest.TrackedTask.ManagerId != user.ManagerId)
                {
                    ViewBag.ErrorMessage = "Недостаточно прав";
                    return View("Error");
                }
                quest.Worker = AppContext.Workers.Find(quest.WorkerId);
                if (quest.IssuedDate >= model.DeadLine)
                {
                    ModelState.AddModelError("DeadLine", "Крайний срок не может быть раньше(одновременно) даты выдачи");
                    model = new QuestModel(quest);
                    return View(model);
                }
                quest.Title = model.Title;
                quest.Description = model.Description;
                if (!quest.IsFinished)
                {
                    quest.DeadLine = model.DeadLine;
                }
                AppContext.Entry(quest).State = EntityState.Modified;
                AppContext.SaveChanges();
                return RedirectToAction("QuestsDetails", "Managers",
                    new {username = username, taskId = taskId, id = quest.Id});
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> QuestsDelete(string username, int taskId, int id)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return PartialView();
            }
            var user = await UserManager.FindByNameAsync(username);
            if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return PartialView();
            }
            if (user.ManagerId == null)
            {
                return RedirectToAction("Index", "Managers");
            }
            var quest = AppContext.Quests.Find(id);
            if (quest == null || quest.TrackedTaskId != taskId)
            {
                ViewBag.ErrorMessage = "Запрошенная подзадача не найдена";
                return View("Error");
            }
            quest.TrackedTask = AppContext.TrackedTasks.Find(taskId);
            if (quest.TrackedTask.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            quest.Worker = AppContext.Workers.Find(quest.WorkerId);
            ViewBag.UserName = username;
            var model = new QuestModel(quest);
            return PartialView(model);
        }

        [HttpPost, ActionName("QuestsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> QuestsDeleteConfirmed(string username, int taskId, int id)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            if (user.ManagerId == null)
            {
                return RedirectToAction("Index", "Managers");
            }
            var quest = AppContext.Quests.Find(id);
            if (quest == null || quest.TrackedTaskId != taskId)
            {
                ViewBag.ErrorMessage = "Запрошенная подзадача не найдена";
                return View("Error");
            }
            quest.TrackedTask = AppContext.TrackedTasks.Find(taskId);
            if (quest.TrackedTask.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return View("Error");
            }
            AppContext.Quests.Remove(quest);
            AppContext.SaveChanges();
            quest.TrackedTask = AppContext.TrackedTasks.Find(taskId);
            var model = new QuestModel(quest){Id = -1};
            return PartialView(model);
        }

        [HttpGet]
        public async Task<ActionResult> QuestsNotify(string username, int taskId, int id)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return PartialView(false);
            }
            var user = await UserManager.FindByNameAsync(username);
            if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return PartialView(false);
            }
            if (user.ManagerId == null)
            {
                return RedirectToAction("Index", "Managers");
            }
            var quest = AppContext.Quests.Find(id);
            if (quest == null || quest.TrackedTaskId != taskId)
            {
                ViewBag.ErrorMessage = "Запрошенная подзадача не найдена";
                return PartialView(false);
            }
            quest.TrackedTask = AppContext.TrackedTasks.Find(taskId);
            if (quest.TrackedTask.ManagerId != user.ManagerId)
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return PartialView(false);
            }
            quest.Worker = AppContext.Workers.Find(quest.WorkerId);
            var worker = await UserManager.FindByIdAsync(quest.Worker.UserId);
            var questUrl = Utility.GetCallbackUrl(Url, "QuestsDetails", "Workers",
                new {username = worker.UserName, id = quest.Id}, Request.Url.Scheme, Request.Url.Host);
            var managerUrl = Utility.GetCallbackUrl(Url, "Details", "Managers", new { username = user.UserName },
                Request.Url.Scheme, Request.Url.Host);
            await
                UserManager.SendEmailAsync(worker.Id, "Напоминание о подзадаче",
                    "Менеджер <a href=\"" + managerUrl + "\">" + user.UserName + "</a>" +
                    " напоминает вам, что вы должны выполнить подзадачу <a href=\"" + questUrl + "\">" + quest.Title +
                    "</a> до " + quest.DeadLine);
            ViewBag.Message = "Исполнителю выслано уведомление";
            return PartialView(true);
        }
    }
}
