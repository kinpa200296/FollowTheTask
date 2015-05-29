using System.Runtime.Serialization;
using FollowTheTaskServiceModels.DataBase;

namespace FollowTheTaskServiceModels.Models
{
    [DataContract]
    public class ManagerModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public TrackedTask[] TrackedTasks { get; set; }

        [DataMember]
        public Worker[] Workers { get; set; }
    }
}
