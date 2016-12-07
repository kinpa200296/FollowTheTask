using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Entities.Issue
{
    [Table("Issues")]
    public class IssueEntity : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public int? TypeId { get; set; }

        public virtual IssueTypeEntity Type { get; set; }

        public int? PriorityId { get; set; }

        public virtual PriorityEntity Priority { get; set; }

        public int? StatusId { get; set; }

        public virtual StatusEntity Status { get; set; }

        public int? ResolutionId { get; set; }

        public virtual ResolutionEntity Resolution { get; set; }

        public DateTimeOffset CreatedDateUtc { get; set; }

        public DateTimeOffset? DeadlineDateUtc { get; set; }

        public int FeatureId { get; set; }

        public virtual FeatureEntity Feature { get; set; }

        public string ReporterName { get; set; }

        public int? ReporterId { get; set; }

        public virtual UserEntity Reporter { get; set; }

        public string AssigneeName { get; set; }

        public int? AssigneeId { get; set; }

        public virtual UserEntity Assignee { get; set; }
    }
}