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

        Group DefaultGroup = new Group()
        {
            Id = 1,
            Language1 = WordkiModel.LanguageType.English,
            Language2 = WordkiModel.LanguageType.Germany,
            Name = "Name",
            CreationDate = new DateTime(1990, 1, 1),
            LastChange = new DateTime(1990, 1, 1),
            State = 1,
            UserId = DatabaseUtil.User.Id,
        };


        public ResultsTests()
        {
        }

        [SetUp]
        public void SetUp()
        {
            Options = DatabaseUtil.GetOptions();
            DatabaseUtil.ClearDatabase(Options);
            DatabaseUtil.SetUser(Options);
            using (var context = new ApplicationDbContext(Options))
            {
                context.Groups.Add(DefaultGroup);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Try to add result to database with wrong userId and groupId. Check if DbUpdateException was thrown
        /// and number of results in database is 0.
        /// </summary>
        [Test]
        public void Try_to_add_result_with_wrong_userId_and_groupId()
        {
            Result result = GetResult();
            result.UserId = 99;
            result.GroupId = 99;
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.AddResult(result);
                Assert.Throws<DbUpdateException>(new TestDelegate(repository.SaveChanges));
            }
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(0, context.Results.Count());
            }
        }

        /// <summary>
        /// Try to add result to database with wrong groupId. Check if DbUpdateException was thrown
        /// and number of results in database is 0.
        /// </summary>
        [Test]
        public void Try_to_add_result_with_wrong_groupId()
        {
            Result result = GetResult();
            result.GroupId = 99;
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.AddResult(result);
                Assert.Throws<DbUpdateException>(new TestDelegate(repository.SaveChanges));
            }
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(0, context.Results.Count());
            }
        }

        /// <summary>
        /// Try to add result to database with wrong userId. Check if DbUpdateException was thrown
        /// and number of results in database is 0.
        /// </summary>
        [Test]
        public void Try_to_add_result_with_wrong_userId()
        {
            Result result = GetResult();
            result.UserId = 99;
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.AddResult(result);
                Assert.Throws<DbUpdateException>(new TestDelegate(repository.SaveChanges));
            }
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(0, context.Results.Count());
            }
        }

        /// <summary>
        /// Try to add single result to database with proper data. Check number of results and 
        /// if existing result in database is equal with this that was added.
        /// </summary>
        [Test]
        public void Try_to_add_result_with_proper_data()
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
                Assert.AreEqual(1, context.Results.Count());
                CheckResults(result, context.Results.First());
            }
        }

        /// <summary>
        /// Try to add result with Id set on 0. Check number of results, if added results in database
        /// has id does not equal 0, and if it is eqaul with this that was added.
        /// </summary>
        [Test]
        public void Try_to_add_result_without_id()
        {
            Result result = GetResult();
            result.Id = 0;
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.AddResult(result);
                repository.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(1, context.Results.Count());
                Result resultFromDatabase = context.Results.First();
                Assert.AreNotEqual(0, resultFromDatabase.Id);
                result.Id = resultFromDatabase.Id;
                CheckResults(result, resultFromDatabase);
            }
        }

        /// <summary>
        /// Try to add couple of result with Id set on 0. Check number of result in database, 
        /// and if all of result have Id not equal 0.
        /// </summary>
        [Test]
        public void Try_to_add_couple_of_results_without_id()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                for (int i = 0; i < 10; i++)
                {
                    Result result = GetResult();
                    result.Id = 0;
                    repository.AddResult(result);
                }
                repository.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(10, context.Results.Count());
                foreach(Result result in context.Results)
                {
                    Assert.AreNotEqual(0, result.Id);
                }
            }
        }

        /// <summary>
        /// Try to update result which exists in database. Check number of resutls and 
        /// if updated result is equal.
        /// </summary>
        [Test]
        public void Try_to_update_result()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                context.Results.Add(GetResult());
                context.SaveChanges();
            }
            Result resulToUpdate = new Result()
            {
                Id = 2,
                UserId = DatabaseUtil.User.Id,
                GroupId = DefaultGroup.Id,
                Accepted = 10,
                Correct = 10,
                Wrong = 10,
                Invisible = 10,
                TimeCount = 10,
                DateTime = new DateTime(1990, 1, 10),
                LastChange = new DateTime(1990, 1, 10),
                LessonType = WordkiModel.Enums.LessonType.FiszkiLesson,
                State = 3,
                TranslationDirection = WordkiModel.Enums.TranslationDirection.FromFirst,
            };

            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                repository.UpdateResult(resulToUpdate);
                repository.SaveChanges();
            }
            using(var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(1, context.Results.Count());
                CheckResults(resulToUpdate, context.Results.First());
            }
        }

        /// <summary>
        /// Try to get resutls from database. Check number of results.
        /// </summary>
        [Test]
        public void Try_to_get_results()
        {
            Result result = GetResult();
            using (var context = new ApplicationDbContext(Options))
            {
                context.Results.Add(result);
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                IWordkiRepo repository = new WordkiRepo(context);
                Assert.AreEqual(1, repository.GetResults().Count());
            }
        }

        public Result GetResult()
        {
            return new Result()
            {
                Id = 2,
                UserId = DatabaseUtil.User.Id,
                GroupId = DefaultGroup.Id,
                Accepted = 1,
                Correct = 1,
                Wrong = 1,
                Invisible = 1,
                TimeCount = 1,
                DateTime = new DateTime(1990, 1, 1),
                LastChange = new DateTime(1990, 1, 1),
                LessonType = WordkiModel.Enums.LessonType.ChoiceLesson,
                State = 1,
                TranslationDirection = WordkiModel.Enums.TranslationDirection.FromSecond,
            };
        }

        public static void CheckResults(Result expected, Result actual)
        {
            Assert.AreEqual(expected.Id, actual.Id, "Error in Id");
            Assert.AreEqual(expected.UserId, actual.UserId, "Error in UserId");
            Assert.AreEqual(expected.GroupId, actual.GroupId, "Error in GroupId");
            Assert.AreEqual(expected.Accepted, actual.Accepted, "Error in Accepted");
            Assert.AreEqual(expected.Correct, actual.Correct, "Error in Correct");
            Assert.AreEqual(expected.Wrong, actual.Wrong, "Error in Wrong");
            Assert.AreEqual(expected.Invisible, actual.Invisible, "Error in Invisible");
            Assert.AreEqual(expected.TimeCount, actual.TimeCount, "Error in TimeCounter");
            Assert.AreEqual(expected.DateTime, actual.DateTime, "Error in DateTime");
            Assert.AreEqual(expected.LastChange, actual.LastChange, "Error in LastChange");
            Assert.AreEqual(expected.State, actual.State, "Error in State");
            Assert.AreEqual(expected.LessonType, actual.LessonType, "Error in LessonType");
            Assert.AreEqual(expected.TranslationDirection, actual.TranslationDirection, "Error in TranslationDirection");
        }

    }
}
