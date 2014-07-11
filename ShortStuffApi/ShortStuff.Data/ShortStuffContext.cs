// ShortStuff.Data
// ShortStuffContext.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Data.Entity;
using ShortStuff.Data.Entities;
using ShortStuff.Data.Mappers;

namespace ShortStuff.Data
{
    public class ShortStuffContext : DbContext
    {
        public ShortStuffContext() : base("ShortStuffContext")
        {
            // Lazy Loading Settings
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShortStuffContext, ShortStuffContextMigrationConfiguration>());
        }

        public DbSet<Echo> Echoes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EchoMapper());
            modelBuilder.Configurations.Add(new MessageMapper());
            modelBuilder.Configurations.Add(new NotificationMapper());
            modelBuilder.Configurations.Add(new TopicMapper());
            modelBuilder.Configurations.Add(new UserMapper());

            base.OnModelCreating(modelBuilder);
        }
    }
}
