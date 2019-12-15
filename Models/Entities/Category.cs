using System;

namespace BeetleTracker.Models.Entities
{
    public enum Category
    {
        Documentation = 0,
        Server = 1,
        Drivers = 2,
        Operations = 4
    }

    public static class CategoryExtension
    {
        public static string GetName(this Category category)
        {
            return Enum.GetName(typeof(Category), category);
        }
    }
}