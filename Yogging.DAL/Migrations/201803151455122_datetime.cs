namespace Yogging.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Story", "CreatedDate", c => c.String());
            AlterColumn("dbo.Story", "LastUpdated", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Story", "LastUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Story", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
