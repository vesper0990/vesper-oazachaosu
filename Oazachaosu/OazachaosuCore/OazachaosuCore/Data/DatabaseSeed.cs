using Repository;
using System;
using System.Linq;

namespace OazachaosuCore.Data
{
    public static class DatabaseSeed
    {

        private static int wordCounter = 1;
        private static int resultCounter = 1;

        public static void Up()
        {
            using (var context = new ApplicationDbContext(ApplicationDbContext.GetOptions()))
            {

                User user = new User()
                {
                    Id = 1,
                    ApiKey = "1",
                    Name = "admin",
                    Password = "asdf"
                };
                context.Users.Add(user);

                for (int i = 1; i <= 1; i++)
                {
                    Group group = new Group()
                    {
                        Id = i,
                        UserId = 1,
                        Language1 = WordkiModel.LanguageType.English,
                        Language2 = WordkiModel.LanguageType.Polish,
                        Name = $"Group {i}",
                        LastChange = new DateTime(2018, 1, 1),
                    };

                    for (int j = 1; j <= 10; j++)
                    {
                        Word word = new Word()
                        {
                            Id = wordCounter++,
                            Comment = $"Comment for word {j}",
                            Language1 = "Language",
                            Language2 = "Język",
                            Language1Comment = "Example",
                            Language2Comment = "Przykład",
                            LastChange = new DateTime(2018, 1, 1),
                        };
                        group.AddWord(word);

                    }

                    for (int j = 1; j <= 10; j++)
                    {
                        Result result = new Result()
                        {
                            Id = resultCounter++,
                            Accepted = 10,
                            Correct = 10,
                            DateTime = new DateTime(1990, 9, 24),
                            Invisible = 10,
                            LessonType = WordkiModel.Enums.LessonType.TypingLesson,
                            TimeCount = 10,
                            TranslationDirection = WordkiModel.Enums.TranslationDirection.FromSecond,
                            Wrong = 10,
                            LastChange = new DateTime(2018, 1, 1),
                        };
                        group.AddResult(result);
                    }
                    context.Groups.Add(group);
                }
                context.SaveChanges();
            }
        }

        public static void Down()
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var group in context.Groups)
                {
                    context.RemoveRange(context.Results.Where(x => x.GroupId == group.Id));
                    context.RemoveRange(context.Words.Where(x => x.GroupId == group.Id));
                }
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext())
            {
                context.RemoveRange(context.Groups);
                context.SaveChanges();
            }
        }
    }
}
