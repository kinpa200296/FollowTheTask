using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FollowTheTask.Identity;
using FollowTheTask.Models.DataBase;
using FollowTheTask.Models.Users;
using Microsoft.AspNet.Identity.Owin;

namespace FollowTheTask.Controllers
{
    [Authorize(Roles = "owner, admin")]
    public class UsersController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private ApplicationRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }

        private ApplicationContext AppContext
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationContext>(); }
        }

        private bool CheckPermission(UserModel user, string roleName)
        {
            if (user != null)
            {
                if (user.UserName == User.Identity.Name)
                    return true;
                if (!user.Roles.First(role => role.Name == "owner").IsUserInRole && User.IsInRole("owner"))
                    return true;
                return !(user.Roles.First(role => role.Name == "owner").IsUserInRole ||
                         user.Roles.First(role => role.Name == "admin").IsUserInRole) && User.IsInRole("admin");
            }
            switch (roleName)
            {
                case "owner":
                    return false;
                case "admin":
                    return User.IsInRole("owner");
            }
            return true;
        }

        private async Task<UserModel> GetUserModel(ApplicationUser user)
        {
            var model = new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            var roles = RoleManager.Roles.AsEnumerable().Select(
                role => new UserRoleModel
                {
                    DisplayName = role.DisplayName,
                    IsUserInRole = false,
                    Name = role.Name
                }).ToArray();
            for (var i = 0; i < roles.Length; i++)
            {
                roles[i].IsUserInRole = await UserManager.IsInRoleAsync(user.Id, roles[i].Name);
            }
            model.Roles = roles;
            return model;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var appUsers = UserManager.Users.ToArray();
            var users = new UserModel[appUsers.Length];
            for (var i = 0; i < users.Length; i++)
            {
                users[i] = await GetUserModel(appUsers[i]);
            }
            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Details(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            var model = await GetUserModel(user);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            var model = await GetUserModel(user);
            if (CheckPermission(model, ""))
                return View(model);
            ViewBag.ErrorMessage = "У вас недостаточно прав";
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string username, UserModel model)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            if (!CheckPermission(await GetUserModel(user), ""))
            {
                ViewBag.ErrorMessage = "У вас недостаточно прав";
                return View("Error");
            }
            if (User.IsInRole("owner"))
            {
                user.UserName = model.UserName;
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                for (int i = 0; i < model.Roles.Length; i++)
                {
                    var role = model.Roles[i];
                    var res = await UserManager.IsInRoleAsync(user.Id, role.Name);
                    if (res == role.IsUserInRole)
                        continue;
                    if (!CheckPermission(null, role.Name))
                    {
                        ModelState.AddModelError("Roles[" + i + "].IsUserInRole", "Недостаточно прав");
                        return View(model);
                    }
                    if (role.IsUserInRole)
                    {
                        result = await UserManager.AddToRoleAsync(user.Id, role.Name);
                        if (result.Succeeded) continue;
                        ViewBag.ErrorMessage = "Во время добавления роли " + role.DisplayName + " произошла ошибка";
                        return View("Error");
                    }
                    result = await UserManager.RemoveFromRoleAsync(user.Id, role.Name);
                    if (result.Succeeded) continue;
                    ViewBag.ErrorMessage = "Во время удаления роли " + role.DisplayName + " произошла ошибка";
                    return View("Error");
                }
                return RedirectToAction("Index", "Users");
            }
            ViewBag.ErrorMessage = "Во время редактирования пользователя произошла ошибка";
            return View("Error");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            var model = await GetUserModel(user);
            if (CheckPermission(model, "") && username != User.Identity.Name)
                return View(model);
            ViewBag.ErrorMessage = "У вас недостаточно прав";
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Пользователя с таким именем не существует";
                return View("Error");
            }
            if (!CheckPermission(await GetUserModel(user), "") || username == User.Identity.Name)
            {
                ViewBag.ErrorMessage = "У вас недостаточно прав";
                return View("Error");
            }
            if (user.WorkerId != null)
            {
                var worker = AppContext.Workers.Find(user.WorkerId);
                worker.Quests = AppContext.Quests.Include(q => q.Worker).Include(q => q.TrackedTask)
                    .Include(q => q.TrackedTask.Manager).Where(q => q.WorkerId == user.WorkerId).ToList();
                foreach (var quest in worker.Quests)
                {
                    AppContext.Quests.Remove(quest);
                }
                AppContext.Workers.Remove(worker);
                AppContext.SaveChanges();
            }
            if (user.ManagerId != null)
            {
                var manager = AppContext.Managers.Find(user.ManagerId);
                manager.TrackedTasks =
                    AppContext.TrackedTasks.Include(t => t.Manager).Where(t => t.ManagerId == user.ManagerId).ToList();
                manager.Workers = AppContext.Workers.Where(w => w.ManagerId == user.ManagerId);
                foreach (var task in manager.TrackedTasks)
                {
                    AppContext.TrackedTasks.Remove(task);
                }
                foreach (var worker in manager.Workers)
                {
                    worker.ManagerId = null;
                    AppContext.Entry(worker).State = EntityState.Modified;
                }
                AppContext.Managers.Remove(manager);
                AppContext.SaveChanges();
            }
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Users");
            }
            ViewBag.ErrorMessage = "Во время удаления пользователя произошла ошибка";
            return View("Error");
        }
    }
}