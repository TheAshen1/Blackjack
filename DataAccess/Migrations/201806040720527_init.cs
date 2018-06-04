namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "IsFinished", c => c.Boolean(nullable: false));
            AddColumn("dbo.Rounds", "WinnerId", c => c.Guid());
            AddColumn("dbo.Players", "IsBot", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Rounds", "WinnerId");
            AddForeignKey("dbo.Rounds", "WinnerId", "dbo.Players", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rounds", "WinnerId", "dbo.Players");
            DropIndex("dbo.Rounds", new[] { "WinnerId" });
            DropColumn("dbo.Players", "IsBot");
            DropColumn("dbo.Rounds", "WinnerId");
            DropColumn("dbo.Games", "IsFinished");
        }
    }
}
