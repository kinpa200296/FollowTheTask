using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities
{
    [Table("AuthData")]
    public class AuthEntity : Entity
    {
        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public string SecurityStamp { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCnt { get; set; }

        public DateTimeOffset? LockoutDateUtc { get; set; }

        public int AccessGrantedTotal { get; set; }

        public DateTimeOffset? LastAccessGrantedDateUtc { get; set; }

        public int AccessFailedTotal { get; set; }

        public DateTimeOffset? LastAccessFailedDateUtc { get; set; }
    }
}