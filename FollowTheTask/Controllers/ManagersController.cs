using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Identity;
using FollowTheTask.Models.DataBase;
using FollowTheTask.Models.Managers;
using Microsoft.AspNet.Identity.Owin;

namespace FollowTheTask.Controllers
{
    [Authorize]
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
            var manager = new ManagerModel(user.Manager);
            return View(manager);
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
    }
}
