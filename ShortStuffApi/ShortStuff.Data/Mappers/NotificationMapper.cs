// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationMapper.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The notification mapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Mappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ShortStuff.Data.Entities;

    /// <summary>
    /// The notification mapper.
    /// </summary>
    internal class NotificationMapper : EntityTypeConfiguration<Notification>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMapper"/> class.
        /// </summary>
        public NotificationMapper()
        {
            // Table Mapping
            ToTable("Notifications");

            // Primary Key
            HasKey(n => n.Id);
            Property(n => n.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            // Properties
            Property(n => n.CreationDate).IsRequired().HasColumnType("smalldatetime");

            Property(n => n.NotificationType).IsRequired();

            Property(n => n.NotificationStatus).IsRequired();

            // Relationships
            HasRequired(n => n.Owner).WithMany(u => u.Notifications).HasForeignKey(n => n.OwnerId).WillCascadeOnDelete(false);

            HasOptional(n => n.SourceMessage).WithMany().HasForeignKey(n => n.SourceMessageId).WillCascadeOnDelete(false);

            HasOptional(n => n.SourceUser).WithMany().HasForeignKey(n => n.SourceUserId).WillCascadeOnDelete(false);

            HasOptional(n => n.SourceTopic).WithMany().HasForeignKey(n => n.SourceTopicId).WillCascadeOnDelete(false);

            HasOptional(n => n.SourceEcho).WithMany().HasForeignKey(n => n.SourceEchoId).WillCascadeOnDelete(false);
        }
    }
}