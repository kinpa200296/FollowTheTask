using System;

namespace FollowTheTask.Models.DataBase
{
    public class Quest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime IssuedDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime DeadLine { get; set; }

        public bool IsFinished { get; set; }

        public int? HoursSpent { get; set; }

        public int TrackedTaskId { get; set; }

        public TrackedTask TrackedTask { get; set; }

        public int WorkerId { get; set; }

        public Worker Worker { get; set; }
    }
}