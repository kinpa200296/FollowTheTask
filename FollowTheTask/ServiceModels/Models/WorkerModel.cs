using System.Runtime.Serialization;
using FollowTheTask.ServiceModels.DataBase;

namespace FollowTheTask.ServiceModels.Models
{
    [DataContract]
    public class WorkerModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public Manager Manager { get; set; }

        [DataMember]
        public Quest[] Quests { get; set; } 
    }
}
