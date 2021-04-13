using System;

namespace GreenFlux.Domain.Exceptions
{
    public class EntityUpdateException : Exception
    {
        public EntityUpdateException(string message)
            : base(message)
        {
        }
    }
}