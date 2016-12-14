using System.Web.Mvc;
using FollowTheTask.Web.Infrastructure;

namespace FollowTheTask.Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}