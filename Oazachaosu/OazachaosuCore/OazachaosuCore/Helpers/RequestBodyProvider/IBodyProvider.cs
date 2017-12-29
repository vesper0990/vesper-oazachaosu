using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OazachaosuCore.Helpers
{
    public interface IBodyProvider
    {
        HttpRequest Request { get; set; }

        string GetBody();
        Task<string> GetBodyAsync();

    }
}
