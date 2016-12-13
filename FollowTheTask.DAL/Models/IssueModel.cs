﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FollowTheTask.DAL.Models
{
    [Table("Issues")]
    public class IssueModel : Model
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public int? TypeId { get; set; }

        public int? PriorityId { get; set; }

        public int? StatusId { get; set; }

        public int? ResolutionId { get; set; }

        public DateTimeOffset CreatedDateUtc { get; set; }

        public DateTimeOffset? DeadlineDateUtc { get; set; }

        public int FeatureId { get; set; }

        public string ReporterName { get; set; }

        public int? ReporterId { get; set; }

        public string AssigneeName { get; set; }

        public int? AssigneeId { get; set; }
    }
}