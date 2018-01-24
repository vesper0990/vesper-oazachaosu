using System.Threading.Tasks;

namespace Oazachaosu.Api.Services
{
    public interface IDataInitializer
    {

        Task SeedAsync();

    }
}
