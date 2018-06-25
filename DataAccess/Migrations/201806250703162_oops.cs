namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oops : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rounds", "RoundNumber", c => c.Int(nullable: false));
            DropColumn("dbo.RoundPlayers", "RoundNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoundPlayers", "RoundNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Rounds", "RoundNumber");
        }
    }
}
