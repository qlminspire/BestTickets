namespace RouteHelpBot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.City", newName: "Cities");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Cities", newName: "City");
        }
    }
}