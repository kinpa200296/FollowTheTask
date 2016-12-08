using System;
using System.Threading.Tasks;

namespace FollowTheTask.DAL.Repositories
{
    public interface IRepository : IDisposable
    {
        int Save();

        Task<int> SaveAsync();
    }
}