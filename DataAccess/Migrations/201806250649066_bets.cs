namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoundPlayers", "RoundNumber", c => c.Int(nullable: false));
            AddColumn("dbo.RoundPlayers", "Chips", c => c.Int(nullable: false));
            AddColumn("dbo.RoundPlayers", "Bet", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoundPlayers", "Bet");
            DropColumn("dbo.RoundPlayers", "Chips");
            DropColumn("dbo.RoundPlayers", "RoundNumber");
        }
    }
}
