// ShortStuff.Data
// ShortStuffContextMigrationConfiguration.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Data.Entity.Migrations;

namespace ShortStuff.Data
{
    internal class ShortStuffContextMigrationConfiguration : DbMigrationsConfiguration<ShortStuffContext>
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
