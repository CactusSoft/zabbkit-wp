using System;

namespace CactusSoft.Stierlitz.Services.Web.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException()
        {
        }

        public AuthorizationException(int code, string message) : base(message)
        {
            Code = code;
        }

        public AuthorizationException(int code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
        }

        public int Code
        {
            get; 
            private set; 
        }
    }
}