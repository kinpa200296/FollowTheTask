using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Identity;
using FollowTheTask.Models.User;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace FollowTheTask.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [HttpGet]
        public async Task<ActionResult> Index(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            var model = new UserModel {UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName};
            return View(model);
        }
    }
}