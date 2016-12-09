using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FollowTheTask.BLL.Services.Auth.ViewModels;
using FollowTheTask.BLL.Services.User.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FollowTheTask.Web.Controllers
{
    public enum ManageMessageId
    {
        ChangePasswordSuccess,
        EditUserSuccess,
        Error
    }


    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }


        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.EditUserSuccess ? "User info has been changed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var model = Mapper.Map<ManageUserViewModel>(user.Dto);

            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, false, false);
                }
                return RedirectToAction("Index", new {Message = ManageMessageId.ChangePasswordSuccess});
            }
            AddErrors(result);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> EditUser()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var model = Mapper.Map<EditUserViewModel>(user.Dto);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(model);
            }
            Mapper.Map(model, user.Dto);
            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", new {Message = ManageMessageId.EditUserSuccess});
            }
            AddErrors(result);
            return View(model);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}