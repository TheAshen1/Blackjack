namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixRoundPlayer : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Round_Player", newName: "RoundPlayers");
            DropForeignKey("dbo.Rounds", "GameId", "dbo.Games");
            DropForeignKey("dbo.Rounds", "WinnerId", "dbo.Players");
            DropIndex("dbo.Rounds", new[] { "GameId" });
            DropIndex("dbo.Rounds", new[] { "WinnerId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Rounds", "WinnerId");
            CreateIndex("dbo.Rounds", "GameId");
            AddForeignKey("dbo.Rounds", "WinnerId", "dbo.Players", "Id");
            AddForeignKey("dbo.Rounds", "GameId", "dbo.Games", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.RoundPlayers", newName: "Round_Player");
        }
    }
}
