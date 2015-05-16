using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Models;
using Microsoft.AspNet.Identity.Owin;

namespace FollowTheTask.Controllers
{
    [Authorize(Roles = "owner, admin")]
    public class RolesController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(RoleManager.Roles.AsEnumerable());
        }

        [HttpGet]
        public async Task<ActionResult> Details(string name)
        {
            if (name == null)
            {
                ViewBag.ErrorMessage = "Имя роли не указано";
                return View("Error");
            }
            var role = await RoleManager.FindByNameAsync(name);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Имя роли не существует";
                return View("Error");
            }
            return
                View(new RoleModel
                {
                    AllowDeletion = role.AllowDeletion,
                    Description = role.Description,
                    DisplayName = role.DisplayName,
                    Name = role.Name
                });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> CheckName(string name)
        {
            var role = await RoleManager.FindByNameAsync(name);
            return Json(role == null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                if (!User.IsInRole("owner"))
                {
                    role.AllowDeletion = true;
                }
                var result =
                    await
                        RoleManager.CreateAsync(new ApplicationRole
                        {
                            AllowDeletion = role.AllowDeletion,
                            Name = role.Name,
                            Description = role.Description,
                            DisplayName = role.DisplayName
                        });
                if (result.Succeeded)
                    return RedirectToAction("Index", "Roles");
            }
            return View(role);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string name)
        {
            if (name == null)
            {
                ViewBag.ErrorMessage = "Имя роли не указано";
                return View("Error");
            }
            var role = await RoleManager.FindByNameAsync(name);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Имя роли не существует";
                return View("Error");
            }
            return
                View(new RoleModel
                {
                    AllowDeletion = role.AllowDeletion,
                    Description = role.Description,
                    DisplayName = role.DisplayName,
                    Name = role.Name
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                if (!User.IsInRole("owner"))
                {
                    role.AllowDeletion = true;
                }
                var appRole = await RoleManager.FindByNameAsync(role.Name);
                appRole.DisplayName = role.DisplayName;
                appRole.Description = role.Description;
                appRole.AllowDeletion = role.AllowDeletion;
                var result = await RoleManager.UpdateAsync(appRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Roles");
                }
            }
            return View(role);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string name)
        {
            if (name == null)
            {
                ViewBag.ErrorMessage = "Имя роли не указано";
                return View("Error");
            }
            var role = await RoleManager.FindByNameAsync(name);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Имя роли не существует";
                return View("Error");
            }
            if ((role.AllowDeletion || User.IsInRole("owner")) && role.Name != "owner")
                return View(role);
            ViewBag.ErrorMessage = "Действие запрещено";
            return View("Error");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            var result = await RoleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }
    }
}
