using FollowTheTask.Models.Common;
using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.TrackedTasks
{
    public class QuestModel : Common.QuestModel
    {
        public QuestModel() { }

        public QuestModel(Quest quest) : base(quest)
        {
            Worker = quest.Worker != null ? new WorkerModel(quest.Worker) : new WorkerModel();
        }

        public WorkerModel Worker { get; set; }
    }
}