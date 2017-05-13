using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jeeb.Gateway.Exceptions
{
    
    public class JeebApiException : Exception
    {
        public JeebApiException(string errorMessage)
            :base(errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public JeebApiException( int errorCode, string errorMessage)
            :base(errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        public int? ErrorCode { private set; get; }

        public string ErrorMessage { private set; get; }
    }
}
