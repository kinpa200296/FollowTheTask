using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Identity;
using FollowTheTask.Models.DataBase;
using FollowTheTask.Models.Workers;
using Microsoft.AspNet.Identity.Owin;
using QuestModel = FollowTheTask.Models.Quests.QuestModel;

namespace FollowTheTask.Controllers
{
    public class WorkersController : Controller
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
            var workers = AppContext.Workers.Include(w => w.Manager).Include(w => w.User).ToList();
            var quests = AppContext.Quests.Include(q => q.TrackedTask).ToLookup(q => q.WorkerId);
            foreach (var worker in workers)
            {
                worker.Quests = quests.Contains(worker.Id)
                    ? quests.First(q => q.Key == worker.Id).AsEnumerable()
                    : null;
            }
            var model = workers.Select(x => new WorkerModel(x));
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
            if (user.WorkerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является исполнителем";
                return View("Error");
            }
            user.Worker =
                AppContext.Workers.Include(m => m.User).Include(m => m.Manager).First(x => x.Id == user.WorkerId);
            user.Worker.Quests = AppContext.Quests.Include(q => q.TrackedTask).Where(q => q.WorkerId == user.WorkerId);
            var worker = new WorkerModel(user.Worker);
            return View(worker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user.WorkerId != null)
            {
                return PartialView(true);
            }
            var worker = new Worker { UserId = user.Id, User = user };
            worker = AppContext.Workers.Add(worker);
            AppContext.SaveChanges();
            user.WorkerId = worker.Id;
            var result = await UserManager.UpdateAsync(user);
            return PartialView(result.Succeeded);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManagerApply(string username)
        {
            var worker = await UserManager.FindByNameAsync(username);
            if (worker == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не существует";
                return View("Error");
            }
            if (worker.WorkerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является исполнителем";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user.ManagerId == null)
            {
                ViewBag.ErrorMessage = "Вы не являетесь менеджером";
                return PartialView(false);
            }
            if (!await UserManager.IsInRoleAsync(user.Id, "manager"))
            {
                ViewBag.ErrorMessage = "Недостаточно прав";
                return PartialView(false);
            }
            user.Manager = AppContext.Managers.Find(user.ManagerId);
            var token = await UserManager.GenerateUserTokenAsync("ApplyToWorker", worker.Id);
            var callbackUrl = Utility.GetCallbackUrl(Url, "ConfirmManager", "Workers",
                new { username = username, managerId = user.Id, token = token },
                Request.Url.Scheme, Request.Url.Host);
            var managerUrl = Utility.GetCallbackUrl(Url, "Details", "Managers", new { username = user.UserName },
                Request.Url.Scheme, Request.Url.Host);
            await
                UserManager.SendEmailAsync(worker.Id, "Подтверждение менеджера",
                    "Менеджер <a href=\"" + managerUrl + "\">" + user.UserName + "</a>" +
                    " хочет работать с Вами. Подтвердите менеджера, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");
            ViewBag.Message =
                "Исполнителю было выслано оповещение. Как только он подтвердит свое участие мы вышлем вам уведомление";
            return PartialView(true);
        }

        [HttpGet]
        public async Task<ActionResult> ConfirmManager(string username, string managerId, string token)
        {
            if (username != User.Identity.Name)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var worker = await UserManager.FindByNameAsync(username);
            var user = await UserManager.FindByIdAsync(managerId);
            worker.Worker = AppContext.Workers.Find(worker.WorkerId);
            var result = await UserManager.VerifyUserTokenAsync(managerId, "ApplyToWorker", token);
            if (!result)
            {
                ViewBag.ErrorMessage = "Ключ подтверждения недействителен.";
                return View(false);
            }
            worker.Worker.ManagerId = worker.ManagerId;
            AppContext.Entry(worker.Worker).State = EntityState.Modified;
            AppContext.SaveChanges();
            var workerUrl = Utility.GetCallbackUrl(Url, "Details", "Workers", new { username = worker.UserName },
                Request.Url.Scheme, Request.Url.Host);
            await
                UserManager.SendEmailAsync(user.Id, "Менеджер подтвержден",
                    "Исполнитель <a href=\"" + workerUrl + "\">" + worker.UserName + "</a> подтвердил свое участие.");
            ViewBag.Message = "Теперь менеджер " + user.UserName + " может выдавать вам подзадания";
            return View(true);
        }

        [HttpGet]
        public async Task<ActionResult> QuestsIndex(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не существует";
                return View("Error");
            }
            if (user.WorkerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является исполнителем";
                return View("Error");
            }
            var quests =
                AppContext.Quests.Include(q => q.Worker)
                    .Include(q => q.TrackedTask)
                    .Include(q => q.TrackedTask.Manager)
                    .Where(q => q.WorkerId == user.WorkerId)
                    .ToList();
            var model = quests.Select(x => new Models.Quests.QuestModel(x));
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> QuestsDetails(string username, int id)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не существует";
                return View("Error");
            }
            if (user.WorkerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является исполнителем";
                return View("Error");
            }
            var quest = AppContext.Quests.Find(id);
            if (quest == null || quest.WorkerId != user.WorkerId)
            {
                ViewBag.ErrorMessage = "Запрошенная подзадача не найдена";
                return View("Error");
            }
            quest.Worker = AppContext.Workers.Find(quest.WorkerId);
            quest.TrackedTask = AppContext.TrackedTasks.Find(quest.TrackedTaskId);
            quest.TrackedTask.Manager = AppContext.Managers.Find(quest.TrackedTask.ManagerId);
            var model = new Models.Quests.QuestModel(quest);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> PassQuestOff(string username, int id)
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
            if (user.WorkerId == null)
            {
                ViewBag.ErrorMessage = "Данный пользователь не является исполнителем";
                return View("Error");
            }
            var quest = AppContext.Quests.Find(id);
            if (quest == null || quest.WorkerId != user.WorkerId)
            {
                ViewBag.ErrorMessage = "Запрошенная подзадача не найдена";
                return View("Error");
            }
            if (quest.IsFinished)
            {
                ViewBag.ErrorMessage = "Подзадача уже сдана";
                return PartialView();
            }
            quest.Worker = AppContext.Workers.Find(quest.WorkerId);
            var model = new QuestModel(quest);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PassQuestOff(string username, int id,
            [Bind(Include = "Title,Description,HoursSpent")] QuestModel model)
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
                if (user.WorkerId == null)
                {
                    ViewBag.ErrorMessage = "Данный пользователь не является исполнителем";
                    return View("Error");
                }
                var quest = AppContext.Quests.Find(id);
                if (quest == null || quest.WorkerId != user.WorkerId)
                {
                    ViewBag.ErrorMessage = "Запрошенная подзадача не найдена";
                    return View("Error");
                }
                if (quest.IsFinished)
                {
                    ViewBag.ErrorMessage = "Подзадача уже сдана";
                    return PartialView();
                }
                quest.Worker = AppContext.Workers.Find(quest.WorkerId);
                var newModel = new QuestModel(quest) {HoursSpent = model.HoursSpent};
                if (model.HoursSpent < 1)
                {
                    ModelState.AddModelError("HoursSpent", "Так мало работать невозможно");
                    return PartialView(newModel);
                }
                var span = DateTime.Now - quest.IssuedDate;
                if (model.HoursSpent > span.TotalHours)
                {
                    ModelState.AddModelError("HoursSpent", "С момента создания подзадачи не прошло столько часов");
                    return PartialView(newModel);
                }
                quest.IsFinished = true;
                quest.CompletionDate = DateTime.Now;
                quest.HoursSpent = model.HoursSpent;
                AppContext.Entry(quest).State = EntityState.Modified;
                AppContext.SaveChanges();
                ViewBag.Message = "Подзадача сдана. Обновите страницу";
                model = new QuestModel(quest);
                return PartialView(model);
            }
            return PartialView(model);
        }
    }
}
