namespace Yogging.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inactiveuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "IsInactive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "IsInactive");
        }
    }
}
