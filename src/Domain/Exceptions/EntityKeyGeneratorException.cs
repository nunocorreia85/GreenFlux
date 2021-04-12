using System;

namespace GreenFlux.Domain.Exceptions
{
    public class EntityKeyGeneratorException : Exception
    {
        public EntityKeyGeneratorException(string message) : base(message)
        {
        }
    }
}