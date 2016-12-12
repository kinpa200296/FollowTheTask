using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Team.ViewModels;
using FollowTheTask.DAL.Repositories.Team;
using FollowTheTask.TransferObjects.Team.DataObjects;

namespace FollowTheTask.BLL.Services.Team
{
    public class TeamService : ModelService<TeamDto, TeamViewModel>, ITeamService
    {
        private readonly ITeamRepository _repository;


        public TeamService(ITeamRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}