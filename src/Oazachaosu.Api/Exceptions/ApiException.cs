using Oazachaosu.Core.Common;
using System;

namespace Oazachaosu.Api.Exceptions
{
    public class ApiException : Exception
    {

        public ErrorCode Code { get; }

        public ApiException()
        {
        }

        public ApiException(ErrorCode code = ErrorCode.Undefined, string message = "", params object[] args) 
            : this(null, code, message, args)
        {
        }

        public ApiException(Exception innerException, ErrorCode code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
