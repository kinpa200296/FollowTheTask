using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Identity;
using FollowTheTask.Models.DataBase;
using FollowTheTask.Models.Quests;
using Microsoft.AspNet.Identity.Owin;

namespace FollowTheTask.Controllers
{
    public class QuestsController : Controller
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
        public async Task<ActionResult> Details(int taskId, int id)
        {
            var trackedTask = AppContext.TrackedTasks.Find(taskId);
            if (trackedTask == null)
            {
                ViewBag.ErrorMessage = "Запрошенная задача не найдена";
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
            quest.TrackedTask = trackedTask;
            var model = new QuestModel(quest);
            return View(model);
        }
    }
}
