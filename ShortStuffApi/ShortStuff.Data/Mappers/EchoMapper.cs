// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EchoMapper.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The echo mapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Mappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ShortStuff.Data.Entities;

    /// <summary>
    /// The echo mapper.
    /// </summary>
    internal class EchoMapper : EntityTypeConfiguration<Echo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EchoMapper"/> class.
        /// </summary>
        public EchoMapper()
        {
            // Table Mapping
            ToTable("Echoes");

            // Primary Key
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            // Properties
            Property(e => e.CreationDate).IsRequired().HasColumnType("smalldatetime");

            // Relationships
            HasRequired(e => e.Creator).WithMany(u => u.Echoes).HasForeignKey(e => e.CreatorId).WillCascadeOnDelete(true); // Delete Echo when Creator is Deleted

            HasRequired(e => e.SourceMessage).WithMany().HasForeignKey(e => e.SourceMessageId).WillCascadeOnDelete(true); // Delete Echo when Source Message is Deleted
        }
    }
}