using System;
using System.Runtime.Serialization;

namespace FollowTheTask.ServiceModels.DataBase
{
    [DataContract]
    public class TrackedTask
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime IssuedDate { get; set; }

        [DataMember]
        public DateTime? CompletionDate { get; set; }

        [DataMember]
        public DateTime DeadLine { get; set; }

        [DataMember]
        public bool IsFinished { get; set; }

        [DataMember]
        public int? HoursSpent { get; set; }

        [DataMember]
        public int ManagerId { get; set; }

        [DataMember]
        public int[] QuestsIds { get; set; } 
    }
}
