using Microsoft.AspNetCore.Http;

namespace Oazachaosu.Api.Helpers
{
    public interface IHeaderElementProvider
    {
        string GetElement(HttpRequest request, string headerTag);
    }
}
