// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShortStuffContext.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The short stuff context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data
{
    using System.Data.Entity;

    using ShortStuff.Data.Entities;
    using ShortStuff.Data.Mappers;

    /// <summary>
    /// The short stuff context.
    /// </summary>
    public class ShortStuffContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortStuffContext"/> class.
        /// </summary>
        public ShortStuffContext()
            : base("ShortStuffContext")
        {
            // Lazy Loading Settings
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShortStuffContext, ShortStuffContextMigrationConfiguration>());
        }

        /// <summary>
        /// Gets or sets the echoes.
        /// </summary>
        public DbSet<Echo> Echoes { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Gets or sets the notifications.
        /// </summary>
        public DbSet<Notification> Notifications { get; set; }

        /// <summary>
        /// Gets or sets the topics.
        /// </summary>
        public DbSet<Topic> Topics { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
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