using System;

namespace GreenFlux.Application.Common.Exceptions
{
    public class AddConnectorException : Exception
    {
        public AddConnectorException(string message) : base(message) 
        {
        }
    }
}