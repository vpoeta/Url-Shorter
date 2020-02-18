using System;

namespace UrlShorter.Services.Application.Common.Exceptions
{
    public class CreateUrlMappingException : Exception
    {
        public CreateUrlMappingException(string message) : base(message)
        {
        }

        public CreateUrlMappingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
