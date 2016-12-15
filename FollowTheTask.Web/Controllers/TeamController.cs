using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FollowTheTask.BLL.Services.Team;
using FollowTheTask.BLL.Services.Team.ViewModels;
using FollowTheTask.TransferObjects.Team.Commands;
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
            if (request.IsFailed) return RedirectToAction("Index", "Error", new { message = request.Message});
            if (request.Value == null) return RedirectToAction("Index", "Error");
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

            model.LeaderId = int.Parse(User.Identity.GetUserId());
            var result = _teamService.CreateModel(model);

            if (result.IsFailed) return RedirectToAction("Index", "Error", new { message = result.Message});
            return RedirectToAction("Index", new {id = model.Id});
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model =  _teamService.GetTeam(new TeamQuery {Id = id});
            if (model.Value != null) return View(model.Value);
            return RedirectToAction("Internal", "Error");
        }

        [HttpPost]
        public ActionResult Edit(TeamInfoViewModel model)
        {
            if (model == null) return RedirectToAction("Index", "Error");
            if (!(model.LeaderId == int.Parse(User.Identity.GetUserId()) || User.IsInRole("Admin")))
                RedirectToAction("AccessViolation", "Error");
            
            var requestResult = _teamService.UpdateModel(Mapper.Map<TeamViewModel>(model));

            if (requestResult.IsFailed) return RedirectToAction("Index", "Error", new {message = requestResult.Message } );
            return RedirectToAction("Index", new {id = model.Id});
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var result = _teamService.DeleteModel(id);
            if (result.IsFailed) return RedirectToAction("Index", "Error", new {message = result.Message});
            return RedirectToAction("Index", "Home");
        }

        public ActionResult BecomeLeader(int teamId)
        {
            var command = _teamService.BecomeLeader(
                new RequestLeadershipCommand {UserId = int.Parse(User.Identity.GetUserId()), TeamId = teamId});
            if (command.IsFailed) return RedirectToAction("Index", "Error", new {message = command.Message});
            return RedirectToAction("Index", new {id = teamId});
        }

        public ActionResult JoinTeam(int teamId)
        {
            var command = _teamService.JoinTeam(
                new RequestJoinTeamCommand {UserId = int.Parse(User.Identity.GetUserId()), TeamId = teamId});
            if (command.IsFailed) return RedirectToAction("Index", "Error", new { message = command.Message });
            return RedirectToAction("Index", new {id = teamId});
        }

        [ChildActionOnly]
        public ActionResult GetTeamFeatures(int teamId)
        {
            var list = _teamService.GetTeamFeatures(new TeamFeaturesQuery() {TeamId = teamId}).Value?.ToArray();
            return PartialView(list);
        }

        [ChildActionOnly]
        public ActionResult GetTeamMembers(int teamId)
        {
            var list = _teamService.GetTeamMembers(new TeamMembersQuery() {TeamId = teamId}).Value?.ToArray();
            return PartialView(list);
        }

        [ChildActionOnly]
        public ActionResult GetAllTeams()
        {
            var list = _teamService.GetAllTeams(new AllTeamsQuery()).Value?.ToArray();
            return PartialView(list);
        }
    }
}