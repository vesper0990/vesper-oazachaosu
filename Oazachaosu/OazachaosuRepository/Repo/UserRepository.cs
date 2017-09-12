using OazachaosuRepository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using OazachaosuRepository.Model;
using System.Threading.Tasks;
using System.Security.Claims;

namespace OazachaosuRepository.Repo
{
    public class UserRepository : IUserRepository
    {
        public UserManager<Model.User> UserManager { get; set; }

        public UserRepository(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        public Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            return UserManager.AddLoginAsync(userId, login);
        }

        public Task<IdentityResult> AddPasswordAsync(string userId, string password)
        {
            return UserManager.AddPasswordAsync(userId, password);
        }

        public Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            return UserManager.ChangePasswordAsync(userId, currentPassword, newPassword);
        }

        public Task<IdentityResult> CreateAsync(User user)
        {
            return UserManager.CreateAsync(user);
        }

        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return UserManager.CreateAsync(user, password);
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
        {
            return UserManager.CreateIdentityAsync(user, authenticationType);
        }

        public void Dispose()
        {
            UserManager.Dispose();
        }

        public Task<User> FindAsync(string userName, string password)
        {
            return UserManager.FindAsync(userName, password);
        }

        public User FindById(string id)
        {
            return UserManager.FindById(id);
        }

        public IList<UserLoginInfo> GetLogins(string userId)
        {
            return UserManager.GetLogins(userId);
        }

        public Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo userLoginInfo)
        {
            return UserManager.RemoveLoginAsync(userId, userLoginInfo);
        }

        Task<ClaimsIdentity> IUserRepository.CreateIdentityAsync(User user, string authenticationType)
        {
            return UserManager.CreateIdentityAsync(user, authenticationType);
        }

        public Task<User> FindAsync(UserLoginInfo userName)
        {
            throw new NotImplementedException();
        }
    }
}