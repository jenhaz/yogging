namespace Yogging.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprintstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sprint", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sprint", "Status");
        }
    }
}
