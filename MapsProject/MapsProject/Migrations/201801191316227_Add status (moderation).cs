namespace MapsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addstatusmoderation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MapObjects", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MapObjects", "Status");
        }
    }
}
