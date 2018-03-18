namespace Yogging.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sprint",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Story",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        Priority = c.Int(),
                        Type = c.Int(),
                        Description = c.String(),
                        AcceptanceCriteria = c.String(),
                        Points = c.Int(),
                        Status = c.Int(),
                        UserId = c.Int(),
                        SprintId = c.Int(),
                        TagId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sprint", t => t.SprintId)
                .ForeignKey("dbo.Tag", t => t.TagId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SprintId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Story", "UserId", "dbo.User");
            DropForeignKey("dbo.Story", "TagId", "dbo.Tag");
            DropForeignKey("dbo.Story", "SprintId", "dbo.Sprint");
            DropIndex("dbo.Story", new[] { "TagId" });
            DropIndex("dbo.Story", new[] { "SprintId" });
            DropIndex("dbo.Story", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Tag");
            DropTable("dbo.Story");
            DropTable("dbo.Sprint");
        }
    }
}
