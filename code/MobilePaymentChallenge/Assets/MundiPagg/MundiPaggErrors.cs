using System;
using System.Collections.Generic;
using System.Text;

namespace MundiPagg
{
    /// <summary>
    /// Error codes returned by MundiPaggAPIs
    /// </summary>
    public enum MundiPaggErrorCode
    {
        SUCCESS         = 0,
        BAD_REQUEST     = 400,
        UNAUTHORIZED    = 401,
        UNKOWN_ERROR    = 500,
    }

    public class MundiPaggError
    {
        public MundiPaggErrorCode ErrorCode;
        
        public Error[] Errors;

        public string GetErrorMessage()
        {
            if (Errors == null)
                return string.Empty;

            string message = string.Empty;
            for(int i = 0; i < Errors.Length; i++)
            {
                message += Errors[i].Message;
                if (i < Errors.Length - 1)
                    message += "\n";
            }
            return message;
        }
        public override string ToString()
        {
            return string.Format("Error Code: {0}\nError Message: {1}", ErrorCode, GetErrorMessage());
        }

        public class Error
        {
            public string Message;
            public string Property;
        }
    }
}
