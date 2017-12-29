using Microsoft.AspNetCore.Http;

namespace OazachaosuCore.Helpers
{
    public interface IHeaderElementProvider
    {
        string GetElement(HttpRequest request, string headerTag);
    }
}
