using System;

namespace BeetleTracker.Models.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
