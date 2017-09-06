using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OazachaosuRepository.Model;
using Repository.Models;
using Repository.Models.Enums;
using Repository.Models.Language;

namespace OazachaosuRepository.Migrations {
  using System.Data.Entity.Migrations;

  internal sealed class Configuration : DbMigrationsConfiguration<OazachaosuRepository.Model.LocalDbContext> {
    public Configuration() {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(OazachaosuRepository.Model.LocalDbContext context) {
      SeedRoles(context);
      SeedUser(context);
      SeedGroup(context);
    }

    private void SeedRoles(Model.LocalDbContext context) {
      var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
      if (!roleManger.RoleExists("admin")) {
        var role = new IdentityRole("admin");
        roleManger.Create(role);
      }
      if (!roleManger.RoleExists("user")) {
        var role = new IdentityRole("user");
        roleManger.Create(role);
      }
    }

    private void SeedUser(Model.LocalDbContext context) {
      var store = new UserStore<Model.User>(context);
      var manager = new UserManager<Model.User>(store);


      if (!context.Users.Any(u => u.UserName == "customUser")) {
        var user = new Model.User() {
          UserName = "customUser",
          LocalId = DateTime.Now.Ticks,
          Password = Hash.GetMd5Hash(MD5.Create(), "qwerty"),
          Name = "customUser",
          ApiKey = "qwerty",
          IsAdmin = false,
          CreateDateTime = DateTime.Now,
          LastLoginDateTime = DateTime.Now,
        };
        var adminresult = manager.Create(user, "qwerty");
        if (adminresult.Succeeded)
          manager.AddToRole(user.Id, "user");
      }
      if (!context.Users.Any(u => u.UserName == "admin")) {
        var user = new Model.User() {
          UserName = "admin",
          LocalId = DateTime.Now.Ticks,
          Password = Hash.GetMd5Hash(MD5.Create(), "Akuku123"),
          Name = "admin",
          ApiKey = "Akuku123",
          IsAdmin = true,
          CreateDateTime = DateTime.Now,
          LastLoginDateTime = DateTime.Now,
        };
        var adminresult = manager.Create(user, "Akuku123");
        if (adminresult.Succeeded)
          manager.AddToRole(user.Id, "admin");
      }
    }

    private void SeedGroup(LocalDbContext context) {
      var user = context.Users.Single(x => x.Name.Equals("admin"));

      for (int i = 0; i < 10; i++) {
        var newGroup = new Group() {
          Id = DateTime.Now.Ticks,
          Name = "Grupa" + i,
          Language1 = LanguageFactory.GetLanguage(LanguageType.English),
          Language2 = LanguageFactory.GetLanguage(LanguageType.French),
          LastUpdateTime = DateTime.Now,
          State = 1,
          UserId = user.LocalId,
        };

        for (int j = 0; j < 20; j++) {
          var newWord = new Word {
            Id = DateTime.Now.Ticks,
            GroupId = newGroup.Id,
            UserId = user.LocalId,
            Language1 = "S³owo" + i,
            Language2 = "Word" + i,
            Drawer = 0,
            State = 1,
            LastUpdateTime = DateTime.Now,
            Visible = true,
          };
          Thread.Sleep(1);
          newGroup.Words.Add(newWord);
        }

        for (int j = 0; j < 20; j++) {
          var newResult = new Result {
            Id = DateTime.Now.Ticks,
            GroupId = newGroup.Id,
            UserId = user.LocalId,
            Correct = (short)j,
            Wrong = (short)j,
            Accepted = (short)j,
            Invisibilities = (short)j,
            State = 1,
            LastUpdateTime = DateTime.Now,
            DateTime = DateTime.Now,
            LessonType = LessonType.FiszkiLesson,
            TimeCount = (short)j,
            TranslationDirection = TranslationDirection.FromFirst,
          };
          Thread.Sleep(1);
          newGroup.Results.Add(newResult);
        }
        context.Groups.AddOrUpdate(newGroup);
      }
      context.SaveChanges();
    }
  }
}
