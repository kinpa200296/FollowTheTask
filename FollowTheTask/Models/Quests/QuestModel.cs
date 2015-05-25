using FollowTheTask.Models.Common;
using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.Quests
{
    public class QuestModel : Common.QuestModel
    {
        public QuestModel() { }

        public QuestModel(Quest quest) : base(quest)
        {
            TrackedTask = quest.TrackedTask != null ? new TrackedTaskModel(quest.TrackedTask) : new TrackedTaskModel();
            Worker = quest.Worker != null ? new WorkerModel(quest.Worker) : new WorkerModel();
        }

        public TrackedTaskModel TrackedTask { get; set; }

        public WorkerModel Worker { get; set; }
    }
}