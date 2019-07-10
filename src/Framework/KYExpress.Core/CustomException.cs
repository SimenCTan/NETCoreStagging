using System;

namespace KYExpress.Core
{
    public class CustomException : Exception
    {
        public int Code { get; set; }

        public CustomException(string message,int code = 1):base(message)
        {
            Code = code;
        }
    }
}
