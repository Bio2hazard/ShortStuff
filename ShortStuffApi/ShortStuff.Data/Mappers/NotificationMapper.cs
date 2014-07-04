using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ShortStuff.Data.Entities;

namespace ShortStuff.Data.Mappers
{
    class NotificationMapper : EntityTypeConfiguration<Notification>
    {
        public NotificationMapper()
        {
            // Table Mapping
            ToTable("Notifications");

            // Primary Key
            HasKey(n => n.Id);
            Property(n => n.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            // Properties
            Property(n => n.CreationDate)
                .IsRequired()
                .HasColumnType("smalldatetime");

            Property(n => n.NotificationType)
                .IsRequired();

            Property(n => n.NotificationStatus)
                .IsRequired();

            // Relationships
            HasRequired(n => n.Owner)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.OwnerId)
                .WillCascadeOnDelete(false);

            HasOptional(n => n.SourceMessage)
                .WithMany()
                .HasForeignKey(n => n.SourceMessageId)
                .WillCascadeOnDelete(false);

            HasOptional(n => n.SourceUser)
                .WithMany()
                .HasForeignKey(n => n.SourceUserId)
                .WillCascadeOnDelete(false);

            HasOptional(n => n.SourceTopic)
                .WithMany()
                .HasForeignKey(n => n.SourceTopicId)
                .WillCascadeOnDelete(false);

            HasOptional(n => n.SourceEcho)
                .WithMany()
                .HasForeignKey(n => n.SourceEchoId)
                .WillCascadeOnDelete(false);
        }
    }
}
