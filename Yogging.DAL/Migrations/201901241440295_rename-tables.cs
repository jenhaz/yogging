namespace Yogging.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renametables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProfileDao", newName: "Profiles");
            RenameTable(name: "dbo.SprintDao", newName: "Sprints");
            RenameTable(name: "dbo.StoryDao", newName: "Stories");
            RenameTable(name: "dbo.TagDao", newName: "Tags");
            RenameTable(name: "dbo.UserDao", newName: "Users");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Users", newName: "UserDao");
            RenameTable(name: "dbo.Tags", newName: "TagDao");
            RenameTable(name: "dbo.Stories", newName: "StoryDao");
            RenameTable(name: "dbo.Sprints", newName: "SprintDao");
            RenameTable(name: "dbo.Profiles", newName: "ProfileDao");
        }
    }
}
