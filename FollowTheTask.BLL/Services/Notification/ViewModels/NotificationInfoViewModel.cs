using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Notification.ViewModels
{
    public class NotificationInfoViewModel : ModelViewModel
    {
        public int TargetId { get; set; }

        [Display(Name = "Target")]
        [StringLength(200)]
        public string Target { get; set; }

        public int ActionSourceId { get; set; }

        public int ActionTypeId { get; set; }

        public int SenderId { get; set; }

        [Display(Name = "Sender")]
        [StringLength(120)]
        public string Sender { get; set; }

        public int ReceiverId { get; set; }

        [Display(Name = "Receiver")]
        [StringLength(120)]
        public string Receiver { get; set; }
    }
}