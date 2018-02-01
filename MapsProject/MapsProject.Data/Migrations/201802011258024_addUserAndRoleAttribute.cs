namespace MapsProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserAndRoleAttribute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Roles", "Name", c => c.String());
        }
    }
}
