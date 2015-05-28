using System.Data.Entity;
using System.Linq;
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
        public ActionResult Index()
        {
            var trackedTasks = AppContext.TrackedTasks.Include(t => t.Manager).ToList();
            var quests = AppContext.Quests.ToLookup(q => q.TrackedTaskId);
            foreach (var task in trackedTasks)
            {
                task.Quests = quests.Contains(task.Id) ? quests.First(q => q.Key == task.Id).AsEnumerable() : null;
                task.Manager.User = AppContext.Users.Find(task.Manager.UserId);
            }
            var model = trackedTasks.Select(x => new TrackedTaskModel(x));
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var trackedTask = AppContext.TrackedTasks.Find(id);
            if (trackedTask == null)
            {
                ViewBag.ErrorMessage = "Запрошенная задача не найдена";
                return View("Error");
            }
            trackedTask.Manager = AppContext.Managers.Find(trackedTask.ManagerId);
            trackedTask.Manager.User = AppContext.Users.Find(trackedTask.Manager.UserId);
            trackedTask.Quests = AppContext.Quests.Include(q => q.Worker).Where(q => q.TrackedTaskId == trackedTask.Id);
            var model = new TrackedTaskModel(trackedTask);
            return View(model);
        }
    }
}
