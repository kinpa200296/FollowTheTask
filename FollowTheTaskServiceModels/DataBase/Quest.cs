using System;
using System.Runtime.Serialization;

namespace FollowTheTaskServiceModels.DataBase
{
    [DataContract]
    public class Quest
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
        public int TrackedTaskId { get; set; }

        [DataMember]
        public int WorkerId { get; set; }
    }
}
