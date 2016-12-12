using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Request.ViewModels
{
    public class RequestViewModel : ModelViewModel
    {
        public int TargetId { get; set; }

        public int ActionTypeId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }
    }
}