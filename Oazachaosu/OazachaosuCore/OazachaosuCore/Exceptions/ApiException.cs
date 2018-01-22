using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OazachaosuCore.Exceptions
{
    public class ApiException : Exception
    {

        public string Code { get; }

        protected ApiException()
        {
        }

        protected ApiException(string code)
        {
            Code = code;
        }

        protected ApiException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected ApiException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected ApiException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected ApiException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }

    }
}
