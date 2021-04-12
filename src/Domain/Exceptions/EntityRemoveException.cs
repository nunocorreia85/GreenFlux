using System;

namespace GreenFlux.Domain.Exceptions
{
    public class EntityRemoveException : Exception
    {
        public EntityRemoveException(string message)
            : base(message)
        {
        }
    }
}