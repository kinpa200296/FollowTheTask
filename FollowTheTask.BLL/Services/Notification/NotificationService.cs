using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Notification.ViewModels;
using FollowTheTask.DAL.Repositories.Notification;
using FollowTheTask.TransferObjects.Notification.DataObjects;

namespace FollowTheTask.BLL.Services.Notification
{
    public class NotificationService : ModelService<NotificationDto, NotificationViewModel>, INotificationService
    {
        private readonly INotificationRepository _repository;


        public NotificationService(INotificationRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}