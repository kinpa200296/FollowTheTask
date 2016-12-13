using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Request.ViewModels
{
    public class RequestInfoViewModel : ModelViewModel
    {
        public int TargetId { get; set; }

        public string Target { get; set; }

        public int ActionTypeId { get; set; }

        public int SenderId { get; set; }

        public string Sender { get; set; }

        public int ReceiverId { get; set; }

        public string Receiver { get; set; }
    }
}