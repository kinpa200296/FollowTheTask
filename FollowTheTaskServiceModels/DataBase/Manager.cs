﻿using System.Runtime.Serialization;

namespace FollowTheTaskServiceModels.DataBase
{
    [DataContract]
    public class Manager
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public int[] TrackedTasksIds { get; set; }

        [DataMember]
        public int[] WorkersIds { get; set; }
    }
}
