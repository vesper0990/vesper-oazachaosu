using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<string>> GetAllUserNames()
        {
            return await Task.FromResult<IEnumerable<string>>(wordkiRepo.GetUsers().Select(x => x.Name));
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

        public async Task RegisterAsync(UserDTO userDto)
        {
            User user = mapper.Map<UserDTO, User>(userDto);
            user.ApiKey = RandomString(32);
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

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
