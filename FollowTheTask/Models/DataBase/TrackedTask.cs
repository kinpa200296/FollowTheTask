using System;
using System.Collections.Generic;

namespace FollowTheTask.Models.DataBase
{
    public class TrackedTask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime IssuedDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime DeadLine { get; set; }

        public bool IsFinished { get; set; }

        public int? HoursSpent { get; set; }

        public int ManagerId { get; set; }

        public Manager Manager { get; set; }

        public IEnumerable<Quest> Quests { get; set; } 
    }
}