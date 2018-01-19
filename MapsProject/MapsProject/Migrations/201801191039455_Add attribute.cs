namespace MapsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addattribute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MapObjects", "ObjectName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.MapObjects", "Category", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MapObjects", "Category", c => c.String());
            AlterColumn("dbo.MapObjects", "ObjectName", c => c.String());
        }
    }
}
