using FollowTheTask.TransferObjects.Model.DataObjects;

namespace FollowTheTask.TransferObjects.Notification.DataObjects
{
    public class NotificationDto : ModelDto
    {
        public int TargetId { get; set; }

        public int ActionSourceId { get; set; }

        public int ActionTypeId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }
    }
}