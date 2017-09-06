namespace OazachaosuRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 32),
                        Content = c.String(maxLength: 512),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 64),
                        Abstract = c.String(maxLength: 512),
                        ContentUrl = c.String(maxLength: 64),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(maxLength: 64),
                        Url = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        Name = c.String(maxLength: 64),
                        Language1 = c.Byte(nullable: false),
                        Language2 = c.Byte(nullable: false),
                        State = c.Int(nullable: false),
                        LastUpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId });
            
            CreateTable(
                "dbo.Result",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        GroupId = c.Long(nullable: false),
                        Correct = c.Short(nullable: false),
                        Accepted = c.Short(nullable: false),
                        Wrong = c.Short(nullable: false),
                        Invisibilities = c.Short(nullable: false),
                        TimeCount = c.Short(nullable: false),
                        TranslationDirection = c.Byte(nullable: false),
                        LessonType = c.Byte(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        LastUpdateTime = c.DateTime(),
                        Group_Id = c.Long(),
                        Group_UserId = c.Long(),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId })
                .ForeignKey("dbo.Group", t => new { t.Group_Id, t.Group_UserId })
                .Index(t => new { t.Group_Id, t.Group_UserId });
            
            CreateTable(
                "dbo.Word",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        GroupId = c.Long(nullable: false),
                        Language1 = c.String(maxLength: 128),
                        Language2 = c.String(maxLength: 128),
                        Drawer = c.Byte(nullable: false),
                        Language1Comment = c.String(maxLength: 128),
                        Language2Comment = c.String(maxLength: 128),
                        Visible = c.Boolean(nullable: false),
                        State = c.Int(nullable: false),
                        LastUpdateTime = c.DateTime(),
                        Group_Id = c.Long(),
                        Group_UserId = c.Long(),
                    })
                .PrimaryKey(t => new { t.Id, t.UserId })
                .ForeignKey("dbo.Group", t => new { t.Group_Id, t.Group_UserId })
                .Index(t => new { t.Group_Id, t.Group_UserId });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LocalId = c.Long(nullable: false),
                        Name = c.String(maxLength: 32),
                        Password = c.String(maxLength: 32),
                        ApiKey = c.String(maxLength: 32),
                        IsAdmin = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        LastLoginDateTime = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ArticleCategory",
                c => new
                    {
                        ArticleId = c.Int(nullable: false),
                        CategoryId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleId, t.CategoryId })
                .ForeignKey("dbo.Article", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Word", new[] { "Group_Id", "Group_UserId" }, "dbo.Group");
            DropForeignKey("dbo.Result", new[] { "Group_Id", "Group_UserId" }, "dbo.Group");
            DropForeignKey("dbo.ArticleCategory", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.ArticleCategory", "ArticleId", "dbo.Article");
            DropIndex("dbo.ArticleCategory", new[] { "CategoryId" });
            DropIndex("dbo.ArticleCategory", new[] { "ArticleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Word", new[] { "Group_Id", "Group_UserId" });
            DropIndex("dbo.Result", new[] { "Group_Id", "Group_UserId" });
            DropTable("dbo.ArticleCategory");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Word");
            DropTable("dbo.Result");
            DropTable("dbo.Group");
            DropTable("dbo.Category");
            DropTable("dbo.Article");
            DropTable("dbo.ArticleComment");
        }
    }
}
