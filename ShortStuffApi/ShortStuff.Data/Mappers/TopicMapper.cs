// ShortStuff.Data
// TopicMapper.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using ShortStuff.Data.Entities;

namespace ShortStuff.Data.Mappers
{
    internal class TopicMapper : EntityTypeConfiguration<Topic>
    {
        public TopicMapper()
        {
            // Table Mapping
            ToTable("Topics");

            // Primary Key
            HasKey(t => t.Id);
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(140)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute
                {
                    IsUnique = true
                }));

            // Relationships
            HasMany(t => t.Messages)
                .WithMany()
                .Map(m => m.MapLeftKey("MessageId"));
        }
    }
}
