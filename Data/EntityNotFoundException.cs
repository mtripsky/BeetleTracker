using System;

namespace BeetleTracker.Data
{
    public class EntityNotFoundException : Exception
    {        
        public EntityNotFoundException(string name, object key)
            : base($"Entity '{name}' ({key}) was not found.")
        {
        }
    }
}