namespace MapsProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtimestamp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MapObjects", "TimeStamp", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MapObjects", "TimeStamp", c => c.DateTime(nullable: false));
        }
    }
}
