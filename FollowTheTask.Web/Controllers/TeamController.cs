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
        public async Task<ActionResult> Index(int id)
        {
            var team = _teamService.GetTeam(new TeamQuery() {Id = id});
            return View(team);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TeamViewModel model)
        {
            if (model != null)
            {
                model.LeaderId = int.Parse(User.Identity.GetUserId());
                var result = _teamService.CreateModelAsync(AutoMapper.Mapper.Map<TeamDto>(model));
                if (result.IsCompleted)
                    return await Index(1/*how to get id???*/);
            }
            return Redirect("Error");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _teamService.GetTeamAsync(new TeamQuery() {Id = id});
            if (model != null) return View(model);
            return Redirect("Error");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TeamViewModel model)
        {
            var requestResult = _teamService.UpdateModel(model);
            if (requestResult.ExecutionComleted)
            {
                return await Index(model.Id);
            }
            return Redirect("Error");
        }

        public ActionResult Delete(int id)
        {
            var result = _teamService.DeleteModel(id);
            if (result.ExecutionComleted)
            {
                return Redirect("Home/Index");
            }
            return Redirect("Error");
        }

        [ChildActionOnly]
        public ActionResult GetTeamFeatures(int teamId)
        {
            var list = _teamService.GetTeamFeatures(new TeamFeaturesQuery() { TeamId = teamId }).Value?.ToArray();
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