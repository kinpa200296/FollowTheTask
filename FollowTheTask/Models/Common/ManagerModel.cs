using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.Common
{
    public class ManagerModel : UserModel
    {
        public ManagerModel()
        {
        }

        public ManagerModel(Manager manager) : base(manager.User)
        {
        }
    }
}