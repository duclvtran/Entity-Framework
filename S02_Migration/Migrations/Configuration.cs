namespace S02_Migration.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<S02_Migration.Context>
    {
        public Configuration()
        {
            //Automatic Migration
            //Autogen when run command
            //Command enable Migrations: enable-migrations –EnableAutomaticMigration:$true
            AutomaticMigrationsEnabled = true; //enable Migration
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "S02_Migration.Context";
        }

        protected override void Seed(S02_Migration.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}