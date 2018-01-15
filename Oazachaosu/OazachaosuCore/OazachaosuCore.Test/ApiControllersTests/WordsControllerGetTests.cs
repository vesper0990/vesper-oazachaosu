﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using OazachaosuCore.Controllers;
using OazachaosuCore.Data;
using OazachaosuCore.Helpers;
using OazachaosuCore.Helpers.Respone;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OazachaosuCore.Test.ApiControllersTests
{
    [TestFixture]
    public class WordsControllerGetTests
    {

        Mock<IHeaderElementProvider> headerElementProviderMock = new Mock<IHeaderElementProvider>();
        DbContextOptions<ApplicationDbContext> Options { get; set; }

        public WordsControllerGetTests()
        {
            Utility.GroupCount = 2;
            Utility.WordCount = 2;
            Utility.ResultCount = 2;
            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "dateTime")).Returns("2000/01/01");
            headerElementProviderMock.Setup(x => x.GetElement(It.IsAny<HttpRequest>(), "apikey")).Returns(DatabaseUtil.User.ApiKey);
        }

        [SetUp]
        public void SetUp()
        {
            Options = DatabaseUtil.GetOptions();
            DatabaseUtil.SetData(Options);
        }

        [Test]
        public void Check_word_count()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult result = new WordsController(new WordkiRepo(context)).Get(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result);

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult);

                Assert.AreEqual(ResultCode.Done, apiResult.Code);

                IEnumerable<Word> words = apiResult.Object as IEnumerable<Word>;
                Assert.AreEqual(Utility.GroupCount * Utility.WordCount, words.Count());
            }
        }

        [Test]
        public void Check_word_count_by_user()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                User user = new User()
                {
                    Id = 2,
                    ApiKey = "key",
                    Name = "name2",
                    Password = "pass"
                };
                context.Users.Add(user);
                Group group = new Group()
                {
                    UserId = 2,
                    Name = "groupa",
                    Id = 999,
                    CreationDate = new DateTime(2018, 1, 1),
                };
                group.AddWord(new Word() { Id = 100, ParentId = group.Id });
                context.Groups.Add(group);
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(Utility.GroupCount * Utility.WordCount + 1, context.Words.Count());
            }
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult result = new WordsController(new WordkiRepo(context)).Get(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result);

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult);

                Assert.AreEqual(ResultCode.Done, apiResult.Code);

                IEnumerable<Word> words = apiResult.Object as IEnumerable<Word>;
                Assert.AreEqual(Utility.GroupCount * Utility.WordCount, words.Count());
            }
        }

        [Test]
        public void Check_word_count_by_last_change()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                Group group = new Group()
                {
                    UserId = 1,
                    Name = "groupa",
                    Id = 999,
                    CreationDate = new DateTime(2018, 1, 1),
                };
                group.AddWord(new Word() { Id = 100, ParentId = group.Id, LastChange = new DateTime(1990, 1, 1) });
                context.Groups.Add(group);
                context.SaveChanges();
            }
            using (var context = new ApplicationDbContext(Options))
            {
                Assert.AreEqual(Utility.GroupCount * Utility.WordCount + 1, context.Words.Count());
            }
            using (var context = new ApplicationDbContext(Options))
            {
                JsonResult result = new WordsController(new WordkiRepo(context)).Get(headerElementProviderMock.Object) as JsonResult;
                Assert.NotNull(result);

                ApiResult apiResult = result.Value as ApiResult;
                Assert.NotNull(apiResult);

                Assert.AreEqual(ResultCode.Done, apiResult.Code);

                IEnumerable<Word> words = apiResult.Object as IEnumerable<Word>;
                Assert.AreEqual(Utility.GroupCount * Utility.WordCount, words.Count());
            }
        }
    }
}