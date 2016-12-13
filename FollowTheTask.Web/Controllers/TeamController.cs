using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FollowTheTask.BLL.Services.Team;
using FollowTheTask.BLL.Services.Team.ViewModels;
using FollowTheTask.TransferObjects.Team.DataObjects;
using FollowTheTask.TransferObjects.Team.Queries;
using Microsoft.AspNet.Identity;

namespace FollowTheTask.Web.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }


        [HttpGet]
        public ActionResult Index(int id)
        {
            var request = _teamService.GetTeam(new TeamQuery() {Id = id});
            if (request.IsFailed) return RedirectToAction("Index", "Error", request.Message);
            if (request.Value == null) RedirectToAction("Index", "Error");
            return View(request.Value);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new TeamViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(TeamViewModel model)
        {
            if (model == null) return RedirectToAction("Index", "Error");
            
            var result = _teamService.CreateModel(model);

            if (result.IsFailed) return RedirectToAction("Internal", "Error");
            return Index(model.Id);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model =  _teamService.GetTeam(new TeamQuery {Id = id});
            if (model != null) return View(model);
            return RedirectToAction("Internal", "Error");
        }

        [HttpPost]
        public ActionResult Edit(TeamViewModel model)
        {
            if (model == null) return RedirectToAction("Index", "Error");
            if (!(model.LeaderId == int.Parse(User.Identity.GetUserId()) || User.IsInRole("Admin")))
                RedirectToAction("AccessViolation", "Error");
            
            var requestResult = _teamService.UpdateModel(model);

            if (requestResult.IsFailed) return RedirectToAction("Index", "Error", requestResult.Message);
            return Index(model.Id);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var result = _teamService.DeleteModel(id);
            if (result.IsFailed) return RedirectToAction("Index", "Error", result.Message);
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult GetTeamFeatures(int teamId)
        {
            var list = _teamService.GetTeamFeatures(new TeamFeaturesQuery() {TeamId = teamId}).Value?.ToArray();
            return View(list);
        }

        [ChildActionOnly]
        public ActionResult GetTeamMembers(int teamId)
        {
            var list = _teamService.GetTeamMembers(new TeamMembersQuery() {TeamId = teamId}).Value?.ToArray();
            return PartialView(list);
        }
    }
}