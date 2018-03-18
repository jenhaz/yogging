namespace Yogging.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storynulls : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Story", "Priority", c => c.Int(nullable: false));
            AlterColumn("dbo.Story", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.Story", "Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Story", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Story", "Status", c => c.Int());
            AlterColumn("dbo.Story", "Points", c => c.Int());
            AlterColumn("dbo.Story", "Type", c => c.Int());
            AlterColumn("dbo.Story", "Priority", c => c.Int());
        }
    }
}
