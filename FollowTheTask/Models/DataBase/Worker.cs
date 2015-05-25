using System.Collections.Generic;

namespace FollowTheTask.Models.DataBase
{
    public class Worker
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int? ManagerId { get; set; }

        public Manager Manager { get; set; }

        public IEnumerable<Quest> Quests { get; set; } 
    }
}