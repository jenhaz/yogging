namespace Yogging.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addprofile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        ImageUrl = c.String(),
                        Blurb = c.String(),
                        LongerBlurb = c.String(),
                        InstagramUsername = c.String(),
                        LinkedInUsername = c.String(),
                        TwitterUsername = c.String(),
                        BlogUrl = c.String(),
                        CurrentJobTitle = c.String(),
                        ContactEmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Profile");
        }
    }
}
