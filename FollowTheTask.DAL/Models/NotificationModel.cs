using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("Notifications")]
    public class NotificationModel : Model
    {
        public int TargetId { get; set; }

        public int ActionSourceId { get; set; }

        public int ActionTypeId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }
    }
}