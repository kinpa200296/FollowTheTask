using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Role.DataObjects;
using FollowTheTask.TransferObjects.Role.Queries;

namespace FollowTheTask.DAL.Repositories.Role
{
    public class RoleRepository : ModelRepository<RoleModel, RoleDto>, IRoleRepository
    {
        public RoleRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        public RoleDto Handle(RoleQuery query)
        {
            RoleModel model = null;
            if (query.Id.HasValue)
            {
                model = ModelsDao.Find(query.Id.Value);
            }
            if (!string.IsNullOrEmpty(query.Name))
            {
                model = ModelsDao.FirstOrDefault(u => u.Name == query.Name);
            }
            return model == null ? null : Mapper.Map<RoleDto>(model);
        }

        public async Task<RoleDto> HandleAsync(RoleQuery query)
        {
            RoleModel model = null;
            if (query.Id.HasValue)
            {
                model = await ModelsDao.FindAsync(query.Id.Value);
            }
            if (!string.IsNullOrEmpty(query.Name))
            {
                model = await ModelsDao.FirstOrDefaultAsync(u => u.Name == query.Name);
            }
            return model == null ? null : Mapper.Map<RoleDto>(model);
        }

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}