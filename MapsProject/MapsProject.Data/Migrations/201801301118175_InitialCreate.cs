namespace MapsProject.Data.Migrations
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
                        ObjectName = c.String(nullable: false, maxLength: 50),
                        GeoLong = c.Double(nullable: false),
                        GeoLat = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        DeleteStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false),
                        DeleteStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagMapObjects",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        MapObject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.MapObject_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.MapObjects", t => t.MapObject_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.MapObject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagMapObjects", "MapObject_Id", "dbo.MapObjects");
            DropForeignKey("dbo.TagMapObjects", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagMapObjects", new[] { "MapObject_Id" });
            DropIndex("dbo.TagMapObjects", new[] { "Tag_Id" });
            DropTable("dbo.TagMapObjects");
            DropTable("dbo.Tags");
            DropTable("dbo.MapObjects");
        }
    }
}
