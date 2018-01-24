using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Helpers
{
    public class SimpleBodyProvider : IBodyProvider
    {

        public string GetBody(HttpRequest request)
        {
            using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public async Task<string> GetBodyAsync(HttpRequest request)
        {
            using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
