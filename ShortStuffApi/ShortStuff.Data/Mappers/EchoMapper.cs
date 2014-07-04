using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ShortStuff.Data.Entities;

namespace ShortStuff.Data.Mappers
{
    class EchoMapper : EntityTypeConfiguration<Echo>
    {
        public EchoMapper()
        {
            // Table Mapping
            ToTable("Echoes");

            // Primary Key
            HasKey(e => e.Id);
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            // Properties
            Property(e => e.CreationDate)
                .IsRequired()
                .HasColumnType("smalldatetime");

            // Relationships
            HasRequired(e => e.Creator)
                .WithMany(u => u.Echoes)
                .HasForeignKey(e => e.CreatorId)
                .WillCascadeOnDelete(true); // Delete Echo when Creator is Deleted

            HasRequired(e => e.SourceMessage)
                .WithMany()
                .HasForeignKey(e => e.SourceMessageId)
                .WillCascadeOnDelete(true); // Delete Echo when Source Message is Deleted
        }
    }
}
