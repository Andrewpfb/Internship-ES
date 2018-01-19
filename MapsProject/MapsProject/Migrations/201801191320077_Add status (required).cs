namespace MapsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addstatusrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MapObjects", "Status", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MapObjects", "Status", c => c.String());
        }
    }
}
