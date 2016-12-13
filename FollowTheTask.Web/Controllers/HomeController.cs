using System.Web.Mvc;
using FollowTheTask.Web.Infrastructure;

namespace FollowTheTask.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var a = StaticValues.Instance.IssueTypes[0];
            return View();
        }
    }
}