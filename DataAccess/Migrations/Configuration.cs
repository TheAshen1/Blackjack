using System.Data.Entity.Migrations;

namespace DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BlackJackContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DataAccess.BlackJackContext";
        }

        protected override void Seed(BlackJackContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
