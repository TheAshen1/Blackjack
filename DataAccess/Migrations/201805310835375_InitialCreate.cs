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
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Round_Player",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                        RoundId = c.Guid(nullable: false),
                        Cards = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Rounds", t => t.RoundId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.RoundId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Round_Player", "RoundId", "dbo.Rounds");
            DropForeignKey("dbo.Round_Player", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Rounds", "GameId", "dbo.Games");
            DropIndex("dbo.Round_Player", new[] { "RoundId" });
            DropIndex("dbo.Round_Player", new[] { "PlayerId" });
            DropIndex("dbo.Rounds", new[] { "GameId" });
            DropTable("dbo.Players");
            DropTable("dbo.Round_Player");
            DropTable("dbo.Rounds");
            DropTable("dbo.Games");
        }
    }
}
