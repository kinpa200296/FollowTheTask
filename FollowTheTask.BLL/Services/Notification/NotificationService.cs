using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Notification.ViewModels;
using FollowTheTask.DAL.Repositories.Notification;
using FollowTheTask.TransferObjects.Notification.DataObjects;
using FollowTheTask.TransferObjects.Notification.Queries;

namespace FollowTheTask.BLL.Services.Notification
{
    public class NotificationService : ModelService<NotificationDto, NotificationViewModel>, INotificationService
    {
        private readonly INotificationRepository _repository;


        public NotificationService(INotificationRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public QueryResult<NotificationInfoDto> GetNotificationDto(NotificationQuery query)
        {
            return RunQuery<NotificationQuery, NotificationInfoDto>(_repository, query);
        }

        public async Task<QueryResult<NotificationInfoDto>> GetNotificationDtoAsync(NotificationQuery query)
        {
            return await RunQueryAsync<NotificationQuery, NotificationInfoDto>(_repository, query);
        }

        public QueryResult<NotificationInfoViewModel> GetNotification(NotificationQuery query)
        {
            return RunQuery<NotificationQuery, NotificationInfoDto>(_repository, query).MapTo<NotificationInfoViewModel>();
        }

        public async Task<QueryResult<NotificationInfoViewModel>> GetNotificationAsync(NotificationQuery query)
        {
            return (await RunQueryAsync<NotificationQuery, NotificationInfoDto>(_repository, query)).MapTo<NotificationInfoViewModel>();
        }

        public ListQueryResult<NotificationInfoDto> GetUserNotificationsDtos(UserNotificationsQuery query)
        {
            return RunListQuery<UserNotificationsQuery, NotificationInfoDto>(_repository, query);
        }

        public async Task<ListQueryResult<NotificationInfoDto>> GetUserNotificationsDtosAsync(UserNotificationsQuery query)
        {
            return await RunListQueryAsync<UserNotificationsQuery, NotificationInfoDto>(_repository, query);
        }

        public ListQueryResult<NotificationInfoViewModel> GetUserNotifications(UserNotificationsQuery query)
        {
            return RunListQuery<UserNotificationsQuery, NotificationInfoDto>(_repository, query).MapTo<NotificationInfoViewModel>();
        }

        public async Task<ListQueryResult<NotificationInfoViewModel>> GetUserNotificationsAsync(UserNotificationsQuery query)
        {
            return (await RunListQueryAsync<UserNotificationsQuery, NotificationInfoDto>(_repository, query)).MapTo<NotificationInfoViewModel>();
        }
    }
}