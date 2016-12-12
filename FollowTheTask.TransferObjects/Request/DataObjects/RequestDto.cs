using FollowTheTask.TransferObjects.Model.DataObjects;

namespace FollowTheTask.TransferObjects.Request.DataObjects
{
    public class RequestDto : ModelDto
    {
        public int TargetId { get; set; }

        public int ActionTypeId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }
    }
}