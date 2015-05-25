using System.Collections.Generic;
using System.Linq;
using FollowTheTask.Models.Common;
using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.TrackedTasks
{
    public class TrackedTaskModel : Common.TrackedTaskModel
    {
        public TrackedTaskModel() { }

        public TrackedTaskModel(TrackedTask trackedTask) : base(trackedTask)
        {
            Manager = trackedTask.Manager != null ? new ManagerModel(trackedTask.Manager) : new ManagerModel();
            Quests = trackedTask.Quests != null
                ? trackedTask.Quests.Select(x => new QuestModel(x))
                : Enumerable.Empty<QuestModel>();
        }

        public ManagerModel Manager { get; set; }

        public IEnumerable<QuestModel> Quests { get; set; }
    }
}