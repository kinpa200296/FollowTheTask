using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Team.ViewModels;
using FollowTheTask.TransferObjects.Team.DataObjects;

namespace FollowTheTask.BLL.Services.Team
{
    public interface ITeamService : IModelService<TeamDto, TeamViewModel>
    {
    }
}