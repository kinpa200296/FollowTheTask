using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Identity;
using FollowTheTask.Models.DataBase;
using FollowTheTask.Models.Workers;
using Microsoft.AspNet.Identity.Owin;

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
            user.Worker = AppContext.Workers.Include(m => m.User).First(x => x.Id == user.WorkerId);
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
    }
}
