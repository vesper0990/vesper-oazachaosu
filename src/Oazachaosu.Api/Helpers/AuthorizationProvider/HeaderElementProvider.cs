using Microsoft.AspNetCore.Http;
using System;

namespace Oazachaosu.Api.Helpers
{
    public class HeaderElementProvider : IHeaderElementProvider
    {
        public string GetElement(HttpRequest request, string headerTag)
        {
            if (!request.Headers.ContainsKey(headerTag))
            {
                throw new Exception($"Tag {headerTag} not exist in request!");
            }
            return request.Headers[headerTag];
        }
    }
}
