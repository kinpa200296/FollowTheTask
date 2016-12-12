using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FollowTheTask.BLL.Services.Team;
using FollowTheTask.TransferObjects.Model.Queries;

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
        public async Task<ActionResult> Index()
        {
            var teams = await _teamService.GetAllModelsAsync(new AllModelsQuery());
            return View(teams.Value.AsEnumerable());
        }
    }
}