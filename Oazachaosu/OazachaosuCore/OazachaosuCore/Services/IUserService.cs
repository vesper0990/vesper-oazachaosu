using System.Collections.Generic;
using System.Threading.Tasks;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Services
{
    public interface IUserService : IService
    {
        Task<IEnumerable<string>> GetAllUserNames();
        Task<UserDTO> GetAsync(string name, string password);
        Task RegisterAsync(UserDTO userDto);
        Task UpdateAsync(string apiKey, string oldPassword, string newPassword);

    }
}
