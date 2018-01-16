using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OazachaosuCore.Data;
using Repository;
using System;
using System.Linq;

namespace OazachaosuCore.Test.WordkiRepositoryTests
{
    [TestFixture]
    public class ResultsTests
    {

        DbContextOptions<ApplicationDbContext> Options { get; set; }
        
        public ResultsTests()
        {

        }

        [SetUp]
        public void SetUp()
        {
            DatabaseUtil.ClearDatabase(Options);
            //DatabaseUtil.SetUser(Options);
        }

        [Test]
        public void Try_to_add_result_with_out_user()
        {
            Result result = GetResult();
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.AddResult(result);
                repository.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(0, context.Results.Count());
            }
        }

        public Result GetResult()
        {
            return new Result()
            {
                Id = 2,
                Accepted = 1,
                Correct = 1,
                Wrong = 1,
                Invisible = 1,
                TimeCount = 1,
                DateTime = DateTime.Now,
                LastChange = DateTime.Now,
                LessonType = WordkiModel.Enums.LessonType.ChoiceLesson,
                State = 1,
                TranslationDirection = WordkiModel.Enums.TranslationDirection.FromSecond,
            };
        }

    }
}
