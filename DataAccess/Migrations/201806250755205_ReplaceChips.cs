namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplaceChips : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Chips", c => c.Int(nullable: false));
            DropColumn("dbo.RoundPlayers", "Chips");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoundPlayers", "Chips", c => c.Int(nullable: false));
            DropColumn("dbo.Players", "Chips");
        }
    }
}
