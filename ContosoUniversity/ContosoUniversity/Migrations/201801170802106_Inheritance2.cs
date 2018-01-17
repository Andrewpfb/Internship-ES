namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "EmailAddress");
        }
    }
}
