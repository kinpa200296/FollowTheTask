using FollowTheTask.Models.Common;
using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.Workers
{
    public class QuestModel : Common.QuestModel
    {
        public QuestModel() { }

        public QuestModel(Quest quest) : base(quest)
        {
            TrackedTask = quest.TrackedTask != null ? new TrackedTaskModel(quest.TrackedTask) : new TrackedTaskModel();
        }

        public TrackedTaskModel TrackedTask { get; set; }
    }
}