// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserMapper.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The user mapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Mappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using ShortStuff.Data.Entities;

    /// <summary>
    /// The user mapper.
    /// </summary>
    internal class UserMapper : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapper"/> class.
        /// </summary>
        public UserMapper()
        {
            // Table Mapping
            ToTable("Users");

            // Primary Key
            HasKey(u => u.Id);
            Property(u => u.Id).HasPrecision(21, 0).HasColumnType("Numeric").IsRequired();

            // Properties
            Property(u => u.Name).IsRequired().HasMaxLength(140);

            Property(u => u.Email).IsRequired().HasMaxLength(255).IsUnicode(false);

            Property(u => u.Tag).IsRequired().IsUnicode(false).HasMaxLength(140).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Tag") { IsUnique = true }));

            Property(u => u.Picture).IsOptional();

            // Relationships
            HasMany(u => u.Followers).WithMany(u => u.Following).Map(u =>
                {
                    u.ToTable("Followers");
                    u.MapLeftKey("FollowerId");
                    u.MapRightKey("FollowingId");
                });

            HasMany(u => u.Favorites).WithMany().Map(u => u.MapLeftKey("MessageId"));

            HasMany(u => u.SubscribedTopics).WithMany(t => t.Subscribers).Map(u =>
                {
                    u.ToTable("TopicSubscribers");
                    u.MapLeftKey("UserId");
                    u.MapRightKey("TopicId");
                });
        }
    }
}