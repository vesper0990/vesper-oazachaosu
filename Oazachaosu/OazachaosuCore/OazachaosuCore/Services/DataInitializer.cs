using Microsoft.EntityFrameworkCore;
using OazachaosuCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OazachaosuCore.Services
{
    public class DataInitializer : IDataInitializer
    {

        private readonly IUserService userService;
        private readonly ApplicationDbContext dbContext;

        public DataInitializer(IUserService userService, ApplicationDbContext dbContext)
        {
            this.userService = userService;
            this.dbContext = dbContext;
        }


        public async Task SeedAsync()
        {
            dbContext.Database.EnsureCreated();
            if((await userService.GetAllUserNames()).Any())
            {
                return;
            }
            for (int i = 1; i <= 10; i++)
            {
                await userService.RegisterAsync(new WordkiModelCore.DTO.UserDTO()
                {
                    Name = $"User{i}",
                    Password = $"SecretPassword{i}",
                });
            }
        }
    }
}
