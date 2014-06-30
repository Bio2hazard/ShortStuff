using System.Data.Entity.Migrations;

namespace ShortStuff.Data
{
    class ShortStuffContextMigrationConfiguration : DbMigrationsConfiguration<ShortStuffContext>
    {
        public ShortStuffContextMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
#if DEBUG
            AutomaticMigrationDataLossAllowed = true;
#else
            AutomaticMigrationDataLossAllowed = false;
#endif
        }

#if DEBUG
        protected override void Seed(ShortStuffContext context)
        {
            new ShortStuffDataSeeder(context).Seed();
        }
#endif
    }
}
