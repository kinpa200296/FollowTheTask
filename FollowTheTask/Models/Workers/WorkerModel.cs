using System.Collections.Generic;
using System.Linq;
using FollowTheTask.Models.Common;
using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.Workers
{
    public class WorkerModel : Common.WorkerModel
    {
        public WorkerModel() { }

        public WorkerModel(Worker worker) : base(worker)
        {
            Manager = worker.Manager != null ? new ManagerModel(worker.Manager) : new ManagerModel();
            Quests = worker.Quests != null
                ? worker.Quests.Select(x => new QuestModel(x))
                : Enumerable.Empty<QuestModel>();
        }

        public ManagerModel Manager { get; set; }

        public IEnumerable<QuestModel> Quests { get; set; }
    }
}