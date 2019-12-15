using System;

namespace BeetleTracker.Models.Entities
{
    public enum IssuePriority
    {
        Trivial = 1,
        Minor = 2,
        Major = 3,
        Critical = 4
    }

    public static class IssuePriorityExtension
    {
        public static string GetName(this IssuePriority priority)
        {
            return Enum.GetName(typeof(IssuePriority), priority);
        }
    }
}
