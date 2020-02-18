using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShorter.Services.Common.Exceptions
{
    public class MongoDbFailureException : Exception
    {
        public MongoDbFailureException(string message) : base(message)
        {
        }

        public MongoDbFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
