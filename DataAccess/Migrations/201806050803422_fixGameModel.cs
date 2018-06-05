namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixGameModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "End", c => c.DateTime());
            DropColumn("dbo.Games", "IsFinished");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "IsFinished", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Games", "End", c => c.DateTime(nullable: false));
        }
    }
}
