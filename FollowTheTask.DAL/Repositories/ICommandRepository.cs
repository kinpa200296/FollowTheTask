using System.Threading.Tasks;
using FollowTheTask.TransferObjects;

namespace FollowTheTask.DAL.Repositories
{
    public interface ICommandRepository<TCommand> where TCommand : Command
    {
        void Execute(TCommand command);

        Task ExecuteAsync(TCommand command);
    }
}