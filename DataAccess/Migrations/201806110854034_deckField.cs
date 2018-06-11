namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deckField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rounds", "Deck", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rounds", "Deck");
        }
    }
}
