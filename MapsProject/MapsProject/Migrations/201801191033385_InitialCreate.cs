namespace MapsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MapObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ObjectName = c.String(),
                        Category = c.String(),
                        GeoLong = c.Double(nullable: false),
                        GeoLat = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MapObjects");
        }
    }
}
