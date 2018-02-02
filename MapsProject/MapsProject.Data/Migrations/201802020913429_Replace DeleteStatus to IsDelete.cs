namespace MapsProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplaceDeleteStatustoIsDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MapObjects", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tags", "IsDelete", c => c.Boolean(nullable: false));
            DropColumn("dbo.MapObjects", "DeleteStatus");
            DropColumn("dbo.Tags", "DeleteStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "DeleteStatus", c => c.Int(nullable: false));
            AddColumn("dbo.MapObjects", "DeleteStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Tags", "IsDelete");
            DropColumn("dbo.MapObjects", "IsDelete");
        }
    }
}
