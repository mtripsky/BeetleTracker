using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeetleTracker.Models.Entities
{
    public enum IssueStatus
    {
        [Description("Open")]
        Open = 1,
        [Description("In progress")]
        [Display(Name="In progress")]
        InProgress = 2,
        [Description("To be tested")]
        [Display(Name = "To be tested")]
        ToBeTested = 3,
        [Description("Reopen")]
        Reopen = 4,
        [Description("Closed")]
        Closed = 5
    }

    public static class IssueStatusExtension
    {
        public static string GetColorBadge(this IssueStatus status)
        {
            switch (status)
            {
                case IssueStatus.Open:
                    return "badge badge-primary";
                case IssueStatus.InProgress:
                    return "badge badge-danger";
                case IssueStatus.ToBeTested:
                    return "badge-issue-status-tobetested";
                case IssueStatus.Reopen:
                    return "badge badge-info";
                case IssueStatus.Closed:
                    return "badge badge-success";
                default:
                    return "badge badge-light";
                    //throw new InvalidEnumArgumentException("Not valid status");
            }
        }
    }
}
