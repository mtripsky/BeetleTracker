using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeetleTracker.Models.Entities
{
    public enum IssueType
    {
        [Description("Task")]
        Task = 1,
        [Description("Bug")]
        Bug = 2,
        [Description("New Feature")]
        [Display(Name="New Feature")]
        NewFeature = 3,
        [Description("Improvements")]
        Improvements = 4
    }
}
