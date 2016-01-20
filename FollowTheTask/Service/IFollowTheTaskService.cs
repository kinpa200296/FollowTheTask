using System.ServiceModel;
using FollowTheTask.ServiceModels.DataBase;
using FollowTheTask.ServiceModels.Models;

namespace FollowTheTask.Service
{
    [ServiceContract]
    public interface IFollowTheTaskService
    {
        [OperationContract]
        User[] GetUsers();

        [OperationContract]
        User GetUserById(string id);

        [OperationContract]
        User GetUserByName(string username);

        [OperationContract]
        Manager[] GetManagers();

        [OperationContract]
        Manager GetManager(int id);

        [OperationContract]
        Worker[] GetWorkers();

        [OperationContract]
        Worker[] GetWorkersForManager(int managerId);

        [OperationContract]
        Worker GetWorker(int id);

        [OperationContract]
        TrackedTask[] GetTrackedTasks();

        [OperationContract]
        TrackedTask[] GetTrackedTasksForManager(int managerId);

        [OperationContract]
        TrackedTask GetTrackedTask(int id);

        [OperationContract]
        Quest[] GetQuests();

        [OperationContract]
        Quest[] GetQuestsForWorker(int workerId);

        [OperationContract]
        Quest[] GetQuestsForTrackedTask(int trackedTaskId);

        [OperationContract]
        Quest GetQuest(int id);

        [OperationContract]
        ManagerModel[] GetManagerModels();

        [OperationContract]
        ManagerModel GetManagerModel(int id);

        [OperationContract]
        WorkerModel[] GetWorkerModels();

        [OperationContract]
        WorkerModel[] GetWorkerModelsForManager(int managerId);

        [OperationContract]
        WorkerModel GetWorkerModel(int id);

        [OperationContract]
        TrackedTaskModel[] GetTrackedTaskModels();

        [OperationContract]
        TrackedTaskModel[] GetTrackedTaskModelsForManager(int managerId);

        [OperationContract]
        TrackedTaskModel GetTrackedTaskModel(int id);

        [OperationContract]
        QuestModel[] GetQuestModels();

        [OperationContract]
        QuestModel[] GetQuestModelsForWorker(int workerId);

        [OperationContract]
        QuestModel[] GetQuestModelsForTrackedTask(int trackedTaskId);

        [OperationContract]
        QuestModel GetQuestModel(int id);
    }
}
