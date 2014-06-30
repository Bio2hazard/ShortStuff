using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ShortStuff.Data.Entities;

namespace ShortStuff.Data.Mappers
{
    class MessageMapper : EntityTypeConfiguration<Message>
    {
        public MessageMapper()
        {
            // Table Mapping
            ToTable("Messages");

            // Primary Key
            HasKey(m => m.Id);
            Property(m => m.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            // Properties
            Property(m => m.CreationDate)
                .IsRequired()
                .HasColumnType("smalldatetime"); // Only use minute-precision

            Property(m => m.MessageContent)
                .IsRequired()
                .HasMaxLength(140); // Only 140 characters for you

            // Relationships
            HasRequired(m => m.Creator)
                .WithMany(u => u.Messages)
                .Map(u => u.MapKey("CreatorID"))
                .WillCascadeOnDelete(false); // Can't use cascade due to cycles / multiple cascade paths, so we will need to be careful

            HasOptional(m => m.ParentMessage)
                .WithMany()
                .WillCascadeOnDelete(false); // Also breaks due to cycles / multiple cascade paths
        }
    }
}
