using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Team.DataObjects;

namespace FollowTheTask.DAL.Repositories.Team
{
    public class TeamRepository : ModelRepository<TeamModel, TeamDto>, ITeamRepository
    {
        public TeamRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}