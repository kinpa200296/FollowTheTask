using System;
using System.Runtime.Serialization;
using FollowTheTask.ServiceModels.DataBase;

namespace FollowTheTask.ServiceModels.Models
{
    [DataContract]
    public class QuestModel
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
        public TrackedTask TrackedTask { get; set; }

        [DataMember]
        public Worker Worker { get; set; }
    }
}
