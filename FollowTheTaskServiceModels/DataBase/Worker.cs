using System.Runtime.Serialization;

namespace FollowTheTaskServiceModels.DataBase
{
    [DataContract]
    public class Worker
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public int? ManagerId { get; set; }

        [DataMember]
        public int[] QuestsIds { get; set; } 
    }
}
