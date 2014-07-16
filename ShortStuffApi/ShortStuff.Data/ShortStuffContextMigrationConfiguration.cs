// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShortStuffContextMigrationConfiguration.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The short stuff context migration configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// The short stuff context migration configuration.
    /// </summary>
    internal class ShortStuffContextMigrationConfiguration : DbMigrationsConfiguration<ShortStuffContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortStuffContextMigrationConfiguration"/> class.
        /// </summary>
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

        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(ShortStuffContext context)
        {
            new ShortStuffDataSeeder(context).Seed();
        }

#endif
    }
}