using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.Common
{
    public class WorkerModel : UserModel
    {
        public WorkerModel()
        {
        }

        public WorkerModel(Worker worker) : base(worker.User)
        {
        }
    }
}