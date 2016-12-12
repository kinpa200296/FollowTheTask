using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Notification.ViewModels
{
    public class NotificationViewModel : ModelViewModel
    {
        public int TargetId { get; set; }

        public int ActionSourceId { get; set; }

        public int ActionTypeId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }
    }
}