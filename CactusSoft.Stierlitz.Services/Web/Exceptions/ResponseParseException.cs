using System;

namespace CactusSoft.Stierlitz.Services.Web.Exceptions
{
    public class ResponseParseException : Exception
    {
        public ResponseParseException()
        {
            
        }

        public ResponseParseException(string message) : base(message)
        {
            
        }

        public ResponseParseException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
