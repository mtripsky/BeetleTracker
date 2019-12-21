using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeetleTracker.Models.Entities
{
    public enum IssuePriority
    {
        [Description("Trivial")]
        Trivial = 1,
        [Description("Minor")]
        Minor = 2,
        [Description("Major")]
        Major = 3,
        [Description("Critical")]
        Critical = 4
    }

    // public static class IssuePriorityExtension
    // {
    //     public static string GetName(this IssuePriority priority)
    //     {
    //         return Enum.GetName(typeof(IssuePriority), priority);
    //     }
    // }
}
