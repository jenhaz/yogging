namespace Yogging.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileDao",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(),
                        ImageUrl = c.String(),
                        Blurb = c.String(),
                        LongerBlurb = c.String(),
                        InstagramUsername = c.String(),
                        LinkedInUsername = c.String(),
                        TwitterUsername = c.String(),
                        BlogUrl = c.String(),
                        GitHubUsername = c.String(),
                        CurrentJobTitle = c.String(),
                        ContactEmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SprintDao",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Story",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                        AcceptanceCriteria = c.String(),
                        Points = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        UserId = c.Guid(),
                        SprintId = c.Guid(),
                        TagId = c.Guid(),
                        SprintDao_Id = c.Guid(),
                        UserDao_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SprintDao", t => t.SprintDao_Id)
                .ForeignKey("dbo.UserDao", t => t.UserDao_Id)
                .Index(t => t.SprintDao_Id)
                .Index(t => t.UserDao_Id);
            
            CreateTable(
                "dbo.StoryDao",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Priority = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        AcceptanceCriteria = c.String(),
                        Points = c.Int(nullable: false),
                        Status = c.String(),
                        UserId = c.Guid(),
                        SprintId = c.Guid(),
                        TagId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SprintDao", t => t.SprintId)
                .ForeignKey("dbo.TagDao", t => t.TagId)
                .ForeignKey("dbo.UserDao", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SprintId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.TagDao",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Colour = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDao",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        IsInactive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoryDao", "UserId", "dbo.UserDao");
            DropForeignKey("dbo.Story", "UserDao_Id", "dbo.UserDao");
            DropForeignKey("dbo.StoryDao", "TagId", "dbo.TagDao");
            DropForeignKey("dbo.StoryDao", "SprintId", "dbo.SprintDao");
            DropForeignKey("dbo.Story", "SprintDao_Id", "dbo.SprintDao");
            DropIndex("dbo.StoryDao", new[] { "TagId" });
            DropIndex("dbo.StoryDao", new[] { "SprintId" });
            DropIndex("dbo.StoryDao", new[] { "UserId" });
            DropIndex("dbo.Story", new[] { "UserDao_Id" });
            DropIndex("dbo.Story", new[] { "SprintDao_Id" });
            DropTable("dbo.UserDao");
            DropTable("dbo.TagDao");
            DropTable("dbo.StoryDao");
            DropTable("dbo.Story");
            DropTable("dbo.SprintDao");
            DropTable("dbo.ProfileDao");
        }
    }
}
