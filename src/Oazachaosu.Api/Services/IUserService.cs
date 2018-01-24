using Oazachaosu.Core;
using Oazachaosu.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Services
{
    public interface IUserService : IService
    {

        Task<User> GetUserAsync(string apiKey);

        Task<IEnumerable<string>> GetAllUserNames();
        Task<bool> IsExists(string name);
        Task<UserDTO> GetAsync(string name, string password);
        Task RegisterAsync(string name, string password);
        Task UpdateAsync(string apiKey, string oldPassword, string newPassword);
    }
}
