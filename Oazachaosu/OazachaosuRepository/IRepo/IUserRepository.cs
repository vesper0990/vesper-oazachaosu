using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OazachaosuRepository.IRepo
{
    public interface IUserRepository
    {

        UserManager<Model.User> UserManager { get; set; }

        Model.User FindById(string id);

        Task<Model.User> FindAsync(string userName, string password);

        Task<Model.User> FindAsync(UserLoginInfo userName);

        Task<IdentityResult> CreateAsync(Model.User user, string password);

        Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo userLoginInfo);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<IdentityResult> AddPasswordAsync(string userId, string password);

        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);

        Task<IdentityResult> CreateAsync(Model.User user);

        Task<ClaimsIdentity> CreateIdentityAsync(Model.User user, string authenticationType);

        IList<UserLoginInfo> GetLogins(string userId);

        void Dispose();
    }
}