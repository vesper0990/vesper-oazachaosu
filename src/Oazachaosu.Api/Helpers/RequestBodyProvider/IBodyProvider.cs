using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Helpers
{
    public interface IBodyProvider
    {
        string GetBody(HttpRequest request);
        Task<string> GetBodyAsync(HttpRequest request);

    }
}
