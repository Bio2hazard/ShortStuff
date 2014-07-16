// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopicMapper.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The topic mapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Mappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ShortStuff.Data.Entities;

    /// <summary>
    /// The topic mapper.
    /// </summary>
    internal class TopicMapper : EntityTypeConfiguration<Topic>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TopicMapper"/> class.
        /// </summary>
        public TopicMapper()
        {
            // Table Mapping
            ToTable("Topics");

            // Primary Key
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            // Properties
            Property(t => t.Name).IsRequired().IsUnicode().HasMaxLength(140).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            // Relationships
            HasMany(t => t.Messages).WithMany().Map(m => m.MapLeftKey("MessageId"));
        }
    }
}