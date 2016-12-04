using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities.Issue
{
    [Table("IssueTypes")]
    public class IssueTypeEntity : Entity
    {
        public string Name { get; set; }
    }
}