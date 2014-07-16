// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageMapper.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The message mapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Mappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ShortStuff.Data.Entities;

    /// <summary>
    /// The message mapper.
    /// </summary>
    internal class MessageMapper : EntityTypeConfiguration<Message>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageMapper"/> class.
        /// </summary>
        public MessageMapper()
        {
            // Table Mapping
            ToTable("Messages");

            // Primary Key
            HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            // Properties
            Property(m => m.CreationDate).IsRequired().HasColumnType("smalldatetime"); // Only use minute-precision

            Property(m => m.MessageContent).IsRequired().HasMaxLength(140); // Only 140 characters for you

            Property(m => m.CreatorId).IsRequired();

            Property(m => m.ParentMessageId).IsOptional();

            // Relationships
            HasRequired(m => m.Creator).WithMany(u => u.Messages).HasForeignKey(m => m.CreatorId).WillCascadeOnDelete(false);
                
                // Can't use cascade due to cycles / multiple cascade paths, so we will need to be careful
            HasOptional(m => m.ParentMessage).WithMany(m => m.Replies).HasForeignKey(m => m.ParentMessageId).WillCascadeOnDelete(false);
                
                // Also breaks due to cycles / multiple cascade paths
        }
    }
}