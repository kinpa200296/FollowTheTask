using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("IssueTypes")]
    public class IssueTypeModel : Model
    {
        public string Name { get; set; }
    }
}