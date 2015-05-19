using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Identity;
using FollowTheTask.Models.ManageAccount;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace FollowTheTask.Controllers
{
    [Authorize]
    public class ManageAccountController : Controller
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
            var requestUser = AuthenticationManager.User;
            if (requestUser.Identity.Name != username)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            var model = new UserModel
            {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string username)
        {
            var requestUser = AuthenticationManager.User;
            if (requestUser.Identity.Name != username)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            var model = new UserModel
            {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string username, UserModel model)
        {
            var requestUser = AuthenticationManager.User;
            if (requestUser.Identity.Name != username)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "ManageAccount", new {username = username});
            }
            ModelState.AddModelError("", "Не удалось обновить данные в базе данных");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> ChangePassword(string username)
        {
            var requestUser = AuthenticationManager.User;
            if (requestUser.Identity.Name != username)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            var model = new ChangePasswordModel {UserName = user.UserName};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(string username, ChangePasswordModel model)
        {
            var requestUser = AuthenticationManager.User;
            if (requestUser.Identity.Name != username)
            {
                ViewBag.ErrorMessage = "Доступ запрещен";
                return View("Error");
            }
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            var result = await UserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "ManageAccount", new { username = username });
            }
            ModelState.AddModelError("OldPassword", "Неверный пароль");
            return View(model);
        } 
    }
}