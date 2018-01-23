using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OazachaosuCore.Extensions;
using OazachaosuCore.Settings;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordkiModelCore.DTO;

namespace OazachaosuCore.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IWordkiRepo wordkiRepo;

        public UserService(IMapper mapper, IWordkiRepo workiRepo,
            GeneralSettings settings)
        {
            this.mapper = mapper;
            this.wordkiRepo = workiRepo;
        }

        public Task<User> GetUserAsync(string apiKey)
        {
            return wordkiRepo.GetUsers().SingleOrDefaultAsync(x => x.ApiKey == apiKey);
        }

        public async Task<IEnumerable<string>> GetAllUserNames()
        {
            return await Task.FromResult<IEnumerable<string>>(wordkiRepo.GetUsers().Select(x => x.Name));
        }

        public async Task<bool> IsExists(string name)
        {
            return await Task.FromResult(wordkiRepo.GetUsers().Any(x => x.Name == name));
        }

        public async Task<UserDTO> GetAsync(string name, string password)
        {
            User user;
            try
            {
                user = await wordkiRepo.GetUsers().SingleOrDefaultAsync(x => x.Name == name && x.Password == password);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return mapper.Map<User, UserDTO>(user);
        }

        public async Task RegisterAsync(string name, string password)
        {
            User user = new User()
            {
                Name = name,
                Password = password
            };
            user.ApiKey = Util.GenerateRandomString(32);
            wordkiRepo.AddUser(user);
            await wordkiRepo.SaveChangesAsync();
        }

        public async Task UpdateAsync(string apiKey, string oldPassword, string newPassword)
        {
            User user = await wordkiRepo.GetUsers().SingleOrDefaultAsync(x => x.ApiKey == apiKey && x.Password == oldPassword);
            user.Password = newPassword;
            wordkiRepo.UpdateUser(user);
            await wordkiRepo.SaveChangesAsync();
        }

    }
}
