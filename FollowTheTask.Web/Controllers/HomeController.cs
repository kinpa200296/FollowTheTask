using System.Web.Mvc;

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