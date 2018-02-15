using NLog;
using Oazachaosu.Api.Data;
using Oazachaosu.Api.Extensions;
using Oazachaosu.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Oazachaosu.Api.Services
{
    public class DataInitializer : IDataInitializer
    {

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IUserService userService;
        private readonly ApplicationDbContext dbContext;

        public DataInitializer(IUserService userService, ApplicationDbContext dbContext)
        {
            this.userService = userService;
            this.dbContext = dbContext;
        }


        public async Task SeedAsync(bool isSeed)
        {
            try
            {
                dbContext.Database.EnsureCreated();
                if (!isSeed)
                {
                    return;
                }
                if ((await userService.GetAllUserNames()).Any())
                {
                    return;
                }
                SeedUser();
                SeedDatas();
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
            }
        }

        private void SeedUser()
        {
            User user = new User
            {
                Id = 1,
                ApiKey = Util.GenerateRandomString(32),
                Name = "admin",
                Password = "password",
                CreationDateTime = DateTime.Now,
            };
            dbContext.Users.Add(user);
        }

        private void SeedDatas()
        {
            Random random = new Random();
            int wordCounter = 1;
            int resultCounter = 1;
            for (int i = 1; i <= 10; i++)
            {
                Group group = new Group()
                {
                    Id = i,
                    UserId = 1,
                    CreationDate = DateTime.Now,
                    Language1 = Core.Common.LanguageType.English,
                    Language2 = Core.Common.LanguageType.Polish,
                    LastChange = DateTime.Now,
                    Name = $"Group {i}",
                    State = int.MaxValue,
                };

                for (int j = 1; j <= 10; j++)
                {
                    Word word = new Word()
                    {
                        Id = wordCounter++,
                        GroupId = i,
                        UserId = 1,
                        Comment = $"Some commen for word {j} in group {i}",
                        Drawer = (byte)random.Next(0, 4),
                        Group = group,
                        IsSelected = random.Next(100) > 80,
                        IsVisible = random.Next(100) > 80,
                        Language1 = $"Word {j}",
                        Language2 = $"Słowo {j}",
                        Language1Comment = $"Some example for word {j} in group {i}",
                        Language2Comment = $"Jakiś przykład dla słowa {j} w grupie {i}",
                        LastChange = DateTime.Now,
                        LastRepeating = new DateTime(),
                        RepeatingCounter = (byte)(random.Next(6) - 1),
                        State = int.MaxValue,
                    };
                    group.AddWord(word);
                }
                for (int j = 1; j <= 10; j++)
                {
                    Result result = new Result()
                    {
                        Id = resultCounter++,
                        Group = group,
                        GroupId = i,
                        Invisible = (short)random.Next(0, 10),
                        Accepted = (short)random.Next(0, 10),
                        Correct = (short)random.Next(0, 10),
                        Wrong = (short)random.Next(0, 10),
                        UserId = 1,
                        TranslationDirection = Core.Common.TranslationDirection.FromFirst,
                        TimeCount = (short)random.Next(300, 600),
                        DateTime = DateTime.Now.AddHours(random.Next(0, 50)),
                        LastChange = DateTime.Now,
                        LessonType = Core.Common.LessonType.FiszkiLesson,
                        State = int.MaxValue,
                    };
                    group.AddResult(result);
                }
                dbContext.Groups.Add(group);
            }
        }
    }
}
