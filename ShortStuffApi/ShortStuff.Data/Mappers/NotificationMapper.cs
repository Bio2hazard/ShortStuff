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
                .Map(u => u.MapKey("OwnerID"))
                .WillCascadeOnDelete(false);

            HasOptional(n => n.SourceMessage)
                .WithMany()
                .Map(m => m.MapKey("SourceMessageID"))
                .WillCascadeOnDelete(false);

            HasOptional(n => n.SourceUser)
                .WithMany()
                .Map(u => u.MapKey("SourceUserID"))
                .WillCascadeOnDelete(false);

            HasOptional(n => n.SourceTopic)
                .WithMany()
                .Map(t => t.MapKey("SourceTopicID"))
                .WillCascadeOnDelete(false);
        }
    }
}
