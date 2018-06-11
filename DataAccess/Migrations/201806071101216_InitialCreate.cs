namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        IsBot = c.Boolean(nullable: false),
                        GameId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.RoundPlayers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                        RoundId = c.Guid(nullable: false),
                        Cards = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .ForeignKey("dbo.Rounds", t => t.RoundId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.RoundId);
            
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GameId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoundPlayers", "RoundId", "dbo.Rounds");
            DropForeignKey("dbo.Rounds", "GameId", "dbo.Games");
            DropForeignKey("dbo.RoundPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Players", "GameId", "dbo.Games");
            DropIndex("dbo.Rounds", new[] { "GameId" });
            DropIndex("dbo.RoundPlayers", new[] { "RoundId" });
            DropIndex("dbo.RoundPlayers", new[] { "PlayerId" });
            DropIndex("dbo.Players", new[] { "GameId" });
            DropTable("dbo.Rounds");
            DropTable("dbo.RoundPlayers");
            DropTable("dbo.Players");
            DropTable("dbo.Games");
        }
    }
}
