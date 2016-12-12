using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Team.DataObjects;

namespace FollowTheTask.DAL.Repositories.Team
{
    public interface ITeamRepository : IModelRepository<TeamDto>
    {
    }
}