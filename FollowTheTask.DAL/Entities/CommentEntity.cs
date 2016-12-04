using System.ComponentModel.DataAnnotations.Schema;
using FollowTheTask.DAL.Entities.Issue;

namespace FollowTheTask.DAL.Entities
{
    [Table("Comments")]
    public class CommentEntity : Entity
    {
        public string Message { get; set; }

        public string DateCreatedUtc { get; set; }

        public int UserId { get; set; }
        
        public virtual UserEntity User { get; set; }

        public int IssueId { get; set; }
        
        public virtual IssueEntity Issue { get; set; }
    }
}