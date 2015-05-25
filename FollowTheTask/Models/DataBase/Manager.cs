using System.Collections.Generic;

namespace FollowTheTask.Models.DataBase
{
    public class Manager
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<TrackedTask> TrackedTasks { get; set; }

        public IEnumerable<Worker> Workers { get; set; } 
    }
}