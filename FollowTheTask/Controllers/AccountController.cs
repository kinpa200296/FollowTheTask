using System;
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
                return View(model);
            }
            var user = new ApplicationUser {UserName = model.UserName, Email = model.Email, EmailConfirmed = false};
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Utility.GetCallbackUrl(Url, "ConfirmEmail", "Account",
                    new {userId = user.Id, token = token},
                    Request.Url.Scheme, Request.Url.Host);
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
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null) 
                return RedirectToAction("Register", "Account");
            if (user.EmailConfirmed)
            {
                ViewBag.Message = "Учетная запись уже подтверждена";
                return View("Info");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, token);
            ViewBag.UserId = userId;
            return View(result.Succeeded);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmEmail(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Utility.GetCallbackUrl(Url, "ConfirmEmail", "Account",
                new {userId = user.Id, token = token},
                Request.Url.Scheme, Request.Url.Host);
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
                var user = await UserManager.FindByNameAsync(model.UserName);
                var userExists = user == null;
                if (userExists)
                {
                    ModelState.AddModelError("UserName", "Такого пользователя не существует");
                    ViewBag.ReturnUrl = returnUrl;
                    return View(model);
                }
                user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("Password", "Неверный пароль");
                }
                else
                {
                    if (!user.EmailConfirmed)
                        return RedirectToAction("ConfirmEmail", "Account", new {userId = user.Id, token = "bad_token"});
                    var claim =
                        await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            IssuedUtc = DateTime.UtcNow,
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(15)
                        }, claim);
                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Main");
                    return Redirect(returnUrl);
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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Пользователя с таким e-mail адресом не зарегистрировано"); 
                return View(model);
            }
            if (!user.EmailConfirmed)
                return RedirectToAction("ConfirmEmail", "Account", new { userId = user.Id, token = "bad_token" });
            var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var callbackUrl = Utility.GetCallbackUrl(Url, "ResetPassword", "Account",
                new {userId = user.Id, token = token},
                Request.Url.Scheme, Request.Url.Host);
            await
                UserManager.SendEmailAsync(user.Id, "Восстановление пароля",
                    "Восстановите ваш пароль, щелкнув <a href=\"" + callbackUrl +
                    "\">здесь</a> <br/> Если вы не запрашивали восстановление пароля, то можете удалить это сообщение");
            ViewBag.Message = "На адрес "+ model.Email +" отправлены дальнейшие инструкции по восстановлению пароля";
            return View("Info");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(string userId, string token)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
                return RedirectToAction("Register", "Account");
            ViewBag.UserId = userId;
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ResetPasswordAsync(model.UserId, model.Token, model.Password);
            if (result.Succeeded)
            {
                ViewBag.Message = "Ваш пароль успешно изменен";
                return View("Info");
            }
            ViewBag.Message = "Восстановления пароля не произошло. Попробуйте еще раз";
            return View("Error");
        }
    }
}