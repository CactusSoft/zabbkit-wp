using System;

namespace CactusSoft.Stierlitz.Services.Web.Exceptions
{
    public class WebServiceException: Exception
    {
        public WebServiceException()
        {
        }

        public WebServiceException(int code, string message) : base(message)
        {
            Code = code;
        }

        public WebServiceException(int code, string message, Exception innerException)
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
