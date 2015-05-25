using System.Collections.Generic;
using System.Linq;
using FollowTheTask.Models.Common;
using FollowTheTask.Models.DataBase;

namespace FollowTheTask.Models.Managers
{
    public class ManagerModel : Common.ManagerModel
    {
        public ManagerModel() { }

        public ManagerModel(Manager manager) : base(manager)
        {
            TrackedTasks = manager.TrackedTasks != null
                ? manager.TrackedTasks.Select(x => new TrackedTaskModel(x))
                : Enumerable.Empty<TrackedTaskModel>();
            Workers = manager.Workers != null
                ? manager.Workers.Select(x => new WorkerModel(x))
                : Enumerable.Empty<WorkerModel>();
        }

        public IEnumerable<TrackedTaskModel> TrackedTasks { get; set; }

        public IEnumerable<WorkerModel> Workers { get; set; } 
    }
}