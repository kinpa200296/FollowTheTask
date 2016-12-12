using System;
using FollowTheTask.TransferObjects.Model.DataObjects;

namespace FollowTheTask.TransferObjects.Comment.DataObjects
{
    public class CommentDto : ModelDto
    {
        public string Message { get; set; }

        public DateTimeOffset DateCreatedUtc { get; set; }

        public string UserName { get; set; }

        public int? UserId { get; set; }

        public int IssueId { get; set; }
    }
}