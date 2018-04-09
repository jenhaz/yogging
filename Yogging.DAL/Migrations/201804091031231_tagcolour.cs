namespace Yogging.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tagcolour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tag", "Colour", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tag", "Colour");
        }
    }
}
