using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OazachaosuCore.Helpers.Respone
{
    public enum ResultCode
    {
        Unknown = -1,
        Done,
        AuthorizationError,
        UserNotFound,
        UserAlreadyExists,
    }
}
