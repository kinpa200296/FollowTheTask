using System.Linq;
using FollowTheTaskServiceModels.DataBase;
using FollowTheTaskServiceModels.Models;

namespace FollowTheTaskService
{
    public class FollowTheTaskService : IFollowTheTaskService
    {
        private FollowTheTask.Models.DataBase.ApplicationContext AppContext =
            FollowTheTask.Models.DataBase.ApplicationContext.Create();

        public User[] GetUsers()
        {
            var appUsers = AppContext.Users.ToArray();
            var users =
                appUsers.Select(
                    x => new User
                        {
                            Id = x.Id,
                            UserName = x.UserName,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            ManagerId = x.ManagerId,
                            WorkerId = x.WorkerId
                        }).ToArray();
            return users;
        }

        public User GetUserById(string id)
        {
            var appUser = AppContext.Users.FirstOrDefault(x => x.Id == id);
            var user = appUser == null
                ? new User { Id="NoSuchUser"}
                : new User
                {
                    Id = appUser.Id,
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    ManagerId = appUser.ManagerId,
                    WorkerId = appUser.WorkerId
                };
            return user;
        }

        public User GetUserByName(string username)
        {
            var appUser = AppContext.Users.FirstOrDefault(x => x.UserName == username);
            var user = appUser == null
                ? new User { Id = "NoSuchUser" }
                : new User
                {
                    Id = appUser.Id,
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    ManagerId = appUser.ManagerId,
                    WorkerId = appUser.WorkerId
                };
            return user;
        }

        public Manager[] GetManagers()
        {
            var appManagers = AppContext.Managers.ToArray();
            var managers = appManagers.Select(
                x => new Manager
                {
                    Id = x.Id,
                    UserId = x.UserId
                }).ToArray();
            var appWorkers = AppContext.Workers.ToLookup(x => x.ManagerId);
            var appTasks = AppContext.TrackedTasks.ToLookup(x => x.ManagerId);
            foreach (var manager in managers)
            {
                manager.WorkersIds = appWorkers.Contains(manager.Id)
                    ? appWorkers.First(x => x.Key == manager.Id).AsEnumerable().Select(x => x.Id).ToArray()
                    : new[] {-1};
                manager.TrackedTasksIds = appTasks.Contains(manager.Id)
                    ? appTasks.First(x => x.Key == manager.Id).AsEnumerable().Select(x => x.Id).ToArray()
                    : new[] {-1};
            }
            return managers;
        }

        public Manager GetManager(int id)
        {
            var appManager = AppContext.Managers.FirstOrDefault(x => x.Id == id);
            var appWorkers = AppContext.Workers.Where(x => x.ManagerId == id).ToArray();
            var appTasks = AppContext.TrackedTasks.Where(x => x.ManagerId == id).ToArray();
            var manager = appManager == null
                ? new Manager {Id = -1}
                : new Manager
                {
                    Id = appManager.Id,
                    UserId = appManager.UserId,
                    WorkersIds = appWorkers.Select(x => x.Id).ToArray(),
                    TrackedTasksIds = appTasks.Select(x => x.Id).ToArray()
                };
            return manager;
        }

        public Worker[] GetWorkers()
        {
            var appWorkers = AppContext.Workers.ToArray();
            var appQuests = AppContext.Quests.ToLookup(x => x.WorkerId);
            var workers =
                appWorkers.Select(
                    x => new Worker
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        ManagerId = x.ManagerId,
                        QuestsIds = appQuests.Contains(x.Id)
                            ? appQuests.First(q => q.Key == x.Id).AsEnumerable().Select(q => q.Id).ToArray()
                            : new[] {-1}
                    }).ToArray();
            return workers;
        }

        public Worker[] GetWorkersForManager(int managerId)
        {
            var manager = GetManager(managerId);
            var workers = new Worker[1];
            if (manager.Id != -1)
            {
                workers = manager.WorkersIds.Select(GetWorker).ToArray();
            }
            return workers;
        }

        public Worker GetWorker(int id)
        {
            var appWorker = AppContext.Workers.FirstOrDefault(x => x.Id == id);
            var worker = appWorker == null
                ? new Worker {Id = -1}
                : new Worker
                {
                    Id = appWorker.Id,
                    UserId = appWorker.UserId,
                    ManagerId = appWorker.ManagerId,
                    QuestsIds = AppContext.Quests.Where(q => q.WorkerId == appWorker.Id).Select(q => q.Id).ToArray()
                };
            return worker;
        }

        public TrackedTask[] GetTrackedTasks()
        {
            var appTasks = AppContext.TrackedTasks.ToArray();
            var appQuests = AppContext.Quests.ToLookup(x => x.TrackedTaskId);
            var tasks = appTasks.Select(
                x => new TrackedTask
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IssuedDate = x.IssuedDate,
                    CompletionDate = x.CompletionDate,
                    DeadLine = x.DeadLine,
                    IsFinished = x.IsFinished,
                    HoursSpent = x.HoursSpent,
                    ManagerId = x.ManagerId,
                    QuestsIds = appQuests.Contains(x.Id)
                        ? appQuests.First(q => q.Key == x.Id).AsEnumerable().Select(q => q.Id).ToArray()
                        : new[] {-1}
                }).ToArray();
            return tasks;
        }

        public TrackedTask[] GetTrackedTasksForManager(int managerId)
        {
            var manager = GetManager(managerId);
            var tasks = new TrackedTask[1];
            if (manager != null)
            {
                tasks = manager.TrackedTasksIds.Select(GetTrackedTask).ToArray();
            }
            return tasks;
        }

        public TrackedTask GetTrackedTask(int id)
        {
            var appTask = AppContext.TrackedTasks.FirstOrDefault(x => x.Id == id);
            var task = appTask == null
                ? new TrackedTask {Id = -1}
                : new TrackedTask
                {
                    Id = appTask.Id,
                    Title = appTask.Title,
                    Description = appTask.Description,
                    IssuedDate = appTask.IssuedDate,
                    CompletionDate = appTask.CompletionDate,
                    DeadLine = appTask.DeadLine,
                    IsFinished = appTask.IsFinished,
                    HoursSpent = appTask.HoursSpent,
                    ManagerId = appTask.ManagerId,
                    QuestsIds = AppContext.Quests.Where(x => x.TrackedTaskId == appTask.Id).Select(x => x.Id).ToArray()
                };
            return task;
        }

        public Quest[] GetQuests()
        {
            var appQuests = AppContext.Quests.ToArray();
            var quests = appQuests.Select(
                x => new Quest
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IssuedDate = x.IssuedDate,
                    CompletionDate = x.CompletionDate,
                    DeadLine = x.DeadLine,
                    IsFinished = x.IsFinished,
                    HoursSpent = x.HoursSpent,
                    TrackedTaskId = x.TrackedTaskId,
                    WorkerId = x.WorkerId
                }).ToArray();
            return quests;
        }

        public Quest[] GetQuestsForWorker(int workerId)
        {
            var worker = GetWorker(workerId);
            var quests = new Quest[1];
            if (worker.Id != -1)
            {
                quests = worker.QuestsIds.Select(GetQuest).ToArray();
            }
            return quests;
        }

        public Quest[] GetQuestsForTrackedTask(int trackedTaskId)
        {
            var task = GetWorker(trackedTaskId);
            var quests = new Quest[1];
            if (task.Id != -1)
            {
                quests = task.QuestsIds.Select(GetQuest).ToArray();
            }
            return quests;
        }

        public Quest GetQuest(int id)
        {
            var appQuest = AppContext.Quests.FirstOrDefault(x => x.Id == id);
            var quest = appQuest == null
                ? new Quest {Id = -1}
                : new Quest
                {
                    Id = appQuest.Id,
                    Title = appQuest.Title,
                    Description = appQuest.Description,
                    IssuedDate = appQuest.IssuedDate,
                    CompletionDate = appQuest.CompletionDate,
                    DeadLine = appQuest.DeadLine,
                    IsFinished = appQuest.IsFinished,
                    HoursSpent = appQuest.HoursSpent,
                    TrackedTaskId = appQuest.TrackedTaskId,
                    WorkerId = appQuest.WorkerId
                };
            return quest;
        }

        public ManagerModel[] GetManagerModels()
        {
            var managers = GetManagers();
            var managerModels = managers.Select(
                x => new ManagerModel
                {
                    Id = x.Id,
                    User = GetUserById(x.UserId),
                    TrackedTasks = GetTrackedTasksForManager(x.Id),
                    Workers = GetWorkersForManager(x.Id)
                }).ToArray();
            return managerModels;
        }

        public ManagerModel GetManagerModel(int id)
        {
            var manager = GetManager(id);
            var managerModel = manager.Id == -1
                ? new ManagerModel {Id = -1}
                : new ManagerModel
                {
                    Id = manager.Id,
                    User = GetUserById(manager.UserId),
                    TrackedTasks = GetTrackedTasksForManager(manager.Id),
                    Workers = GetWorkersForManager(manager.Id)
                };
            return managerModel;
        }

        public WorkerModel[] GetWorkerModels()
        {
            var workers = GetWorkers();
            var workerModels = workers.Select(
                x => new WorkerModel
                {
                    Id = x.Id,
                    User = GetUserById(x.UserId),
                    Manager = GetManager(x.ManagerId ?? -1),
                    Quests = GetQuestsForWorker(x.Id)
                }).ToArray();
            return workerModels;
        }

        public WorkerModel[] GetWorkerModelsForManager(int managerId)
        {
            var workers = GetWorkersForManager(managerId);
            var workerModels = workers.Select(
                x => new WorkerModel
                {
                    Id = x.Id,
                    User = GetUserById(x.UserId),
                    Manager = GetManager(x.ManagerId ?? -1),
                    Quests = GetQuestsForWorker(x.Id)
                }).ToArray();
            return workerModels;
        }

        public WorkerModel GetWorkerModel(int id)
        {
            var worker = GetWorker(id);
            var workerModel = worker.Id == -1
                ? new WorkerModel {Id = -1}
                : new WorkerModel
                {
                    Id = worker.Id,
                    User = GetUserById(worker.UserId),
                    Manager = GetManager(worker.ManagerId ?? -1),
                    Quests = GetQuestsForWorker(worker.Id)
                };
            return workerModel;
        }

        public TrackedTaskModel[] GetTrackedTaskModels()
        {
            var tasks = GetTrackedTasks();
            var taskModels = tasks.Select(
                x => new TrackedTaskModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IssuedDate = x.IssuedDate,
                    CompletionDate = x.CompletionDate,
                    DeadLine = x.DeadLine,
                    IsFinished = x.IsFinished,
                    HoursSpent = x.HoursSpent,
                    Manager = GetManager(x.ManagerId),
                    Quests = GetQuestsForTrackedTask(x.Id)
                }).ToArray();
            return taskModels;
        }

        public TrackedTaskModel[] GetTrackedTaskModelsForManager(int managerId)
        {
            var tasks = GetTrackedTasksForManager(managerId);
            var taskModels = tasks.Select(
                x => new TrackedTaskModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IssuedDate = x.IssuedDate,
                    CompletionDate = x.CompletionDate,
                    DeadLine = x.DeadLine,
                    IsFinished = x.IsFinished,
                    HoursSpent = x.HoursSpent,
                    Manager = GetManager(x.ManagerId),
                    Quests = GetQuestsForTrackedTask(x.Id)
                }).ToArray();
            return taskModels;
        }

        public TrackedTaskModel GetTrackedTaskModel(int id)
        {
            var task = GetTrackedTask(id);
            var taskModel = task.Id == -1
                ? new TrackedTaskModel {Id = -1}
                : new TrackedTaskModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    IssuedDate = task.IssuedDate,
                    CompletionDate = task.CompletionDate,
                    DeadLine = task.DeadLine,
                    IsFinished = task.IsFinished,
                    HoursSpent = task.HoursSpent,
                    Manager = GetManager(task.ManagerId),
                    Quests = GetQuestsForTrackedTask(task.Id)
                };
            return taskModel;
        }

        public QuestModel[] GetQuestModels()
        {
            var quests = GetQuests();
            var questModels = quests.Select(
                x => new QuestModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IssuedDate = x.IssuedDate,
                    CompletionDate = x.CompletionDate,
                    DeadLine = x.DeadLine,
                    IsFinished = x.IsFinished,
                    HoursSpent = x.HoursSpent,
                    TrackedTask = GetTrackedTask(x.TrackedTaskId),
                    Worker = GetWorker(x.WorkerId)
                }).ToArray();
            return questModels;
        }

        public QuestModel[] GetQuestModelsForWorker(int workerId)
        {
            var quests = GetQuestsForWorker(workerId);
            var questModels = quests.Select(
                x => new QuestModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IssuedDate = x.IssuedDate,
                    CompletionDate = x.CompletionDate,
                    DeadLine = x.DeadLine,
                    IsFinished = x.IsFinished,
                    HoursSpent = x.HoursSpent,
                    TrackedTask = GetTrackedTask(x.TrackedTaskId),
                    Worker = GetWorker(x.WorkerId)
                }).ToArray();
            return questModels;
        }

        public QuestModel[] GetQuestModelsForTrackedTask(int trackedTaskId)
        {
            var quests = GetQuestsForTrackedTask(trackedTaskId);
            var questModels = quests.Select(
                x => new QuestModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IssuedDate = x.IssuedDate,
                    CompletionDate = x.CompletionDate,
                    DeadLine = x.DeadLine,
                    IsFinished = x.IsFinished,
                    HoursSpent = x.HoursSpent,
                    TrackedTask = GetTrackedTask(x.TrackedTaskId),
                    Worker = GetWorker(x.WorkerId)
                }).ToArray();
            return questModels;
        }

        public QuestModel GetQuestModel(int id)
        {
            var quest = GetQuest(id);
            var questModel = quest.Id == -1
                ? new QuestModel {Id = -1}
                : new QuestModel
                {
                    Id = quest.Id,
                    Title = quest.Title,
                    Description = quest.Description,
                    IssuedDate = quest.IssuedDate,
                    CompletionDate = quest.CompletionDate,
                    DeadLine = quest.DeadLine,
                    IsFinished = quest.IsFinished,
                    HoursSpent = quest.HoursSpent,
                    TrackedTask = GetTrackedTask(quest.TrackedTaskId),
                    Worker = GetWorker(quest.WorkerId)
                };
            return questModel;
        }
    }
}
