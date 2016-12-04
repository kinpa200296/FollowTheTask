using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities
{
    [Table("Requests")]
    public class RequestEntity : Entity
    {
        public int TargetId { get; set; }

        public int ActionTypeId { get; set; }

        public virtual ActionTypeEntity ActionType { get; set; }

        public int SenderId { get; set; }

        public virtual UserEntity Sender { get; set; }

        public int ReceiverId { get; set; }

        public virtual UserEntity Receiver { get; set; }
    }
}