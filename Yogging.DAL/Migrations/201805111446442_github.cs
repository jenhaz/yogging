namespace Yogging.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class github : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profile", "GitHubUsername", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profile", "GitHubUsername");
        }
    }
}
