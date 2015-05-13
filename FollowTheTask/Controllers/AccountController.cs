using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace FollowTheTask.Controllers
{
    [Authorize]
    public class AccountController : Controller
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
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var temp = await UserManager.FindByNameAsync(model.UserName);
            if (temp != null)
            {
                ModelState.AddModelError("UserName", "Имя пользователя уже занято");
                return View(model);
            }
            temp = await UserManager.FindByEmailAsync(model.Email);
            if (temp != null)
            {
                ModelState.AddModelError("Email", "На данный e-mail уже зарегистрирован пользователь");
            }
            var user = new ApplicationUser {UserName = model.UserName, Email = model.Email, EmailConfirmed = false};
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code });
                await
                    UserManager.SendEmailAsync(user.Id, "Подтверждение учетной записи",
                        "Подтвердите вашу учетную запись, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");
                ViewBag.Message = "На почтовый адрес " + user.Email +
                                  " Вам высланы дальнейшие инструкции по завершению регистрации";
                return View("Info");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null) 
                return RedirectToAction("Register", "Account");
            if (user.EmailConfirmed)
            {
                ViewBag.Message = "Учетная запись уже подтверждена";
                return View("Info");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            ViewBag.UserId = userId;
            return View(result.Succeeded);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmEmail(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code });
            await
                UserManager.SendEmailAsync(user.Id, "Подтверждение учетной записи",
                    "Подтвердите вашу учетную запись, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");
            ViewBag.Message = "На почтовый адрес " + user.Email +
                              " Вам высланы дальнейшие инструкции по завершению регистрации";
            return View("Info");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
                else
                {
                    var claim =
                        await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = true}, claim);
                    if (string.IsNullOrEmpty(returnUrl))
                        RedirectToAction("Index", "Main");
                    else Redirect(returnUrl);
                }
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Main");
        }
    }
}