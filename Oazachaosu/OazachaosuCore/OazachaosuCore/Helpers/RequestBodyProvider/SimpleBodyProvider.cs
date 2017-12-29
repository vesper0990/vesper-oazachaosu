using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OazachaosuCore.Helpers
{
    public class SimpleBodyProvider : IBodyProvider
    {

        public HttpRequest Request { get; set; }

        public string GetBody()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public async Task<string> GetBodyAsync()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
