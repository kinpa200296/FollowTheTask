using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("Comments")]
    public class CommentModel : Model
    {
        public string Message { get; set; }

        public DateTimeOffset DateCreatedUtc { get; set; }

        public string UserName { get; set; }

        public int? UserId { get; set; }

        public int IssueId { get; set; }
    }
}