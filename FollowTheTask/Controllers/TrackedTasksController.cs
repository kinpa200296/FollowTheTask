using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Identity;
using FollowTheTask.Models.DataBase;
using FollowTheTask.Models.TrackedTasks;
using Microsoft.AspNet.Identity.Owin;

namespace FollowTheTask.Controllers
{
    public class TrackedTasksController : Controller
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
        public async Task<ActionResult> Index(string username)
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
            var model = trackedTasks.Select(x => new TrackedTaskModel(x));
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(string username, int id)
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
            var model = new TrackedTaskModel(trackedTask);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Create(string username)
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
        public async Task<ActionResult> Create(string username,
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
                    ManagerId = (int) user.ManagerId,
                    IsFinished = false
                };
                AppContext.TrackedTasks.Add(trackedTask);
                AppContext.SaveChanges();
                return RedirectToAction("Index", "TrackedTasks", new {username = username});
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string username, int id)
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
        public async Task<ActionResult> Edit(string username,
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
                return RedirectToAction("Details", "TrackedTasks", new{username = username, id = model.Id});
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string username, int id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string username, int id)
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
            AppContext.TrackedTasks.Remove(trackedTask);
            AppContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
