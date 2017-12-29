using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OazachaosuCore.Helpers
{
    public interface IBodyProvider
    {
        string GetBody(HttpRequest request);
        Task<string> GetBodyAsync(HttpRequest request);

    }
}
