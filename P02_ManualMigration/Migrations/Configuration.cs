namespace P02_ManualMigration.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<P02_ManualMigration.Context>
    {
        public Configuration()
        {
            //Manual Migration
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(P02_ManualMigration.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}