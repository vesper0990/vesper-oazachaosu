using OazachaosuRepository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using OazachaosuRepository.Model;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OazachaosuRepository.Repo
{
    public class UserRepositoryTest : IUserRepository
    {
        public UserManager<Model.User> UserManager { get; set; }

        public Task<IdentityResult> AddLoginAsync(User userId, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> AddPasswordAsync(string userId, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<User> FindAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public User FindById(string id)
        {
            return new Model.User()
            {
                Id = "1",
                UserName = "admin",
                LocalId = 1,
                Password = Hash.GetMd5Hash(System.Security.Cryptography.MD5.Create(), "Akuku123"),
                Name = "admin",
                ApiKey = "Akuku123",
                IsAdmin = true,
                CreateDateTime = DateTime.Now,
                LastLoginDateTime = DateTime.Now,
            };
        }

        public IList<UserLoginInfo> GetLogins(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo userLoginInfo)
        {
            throw new NotImplementedException();
        }
    }
}