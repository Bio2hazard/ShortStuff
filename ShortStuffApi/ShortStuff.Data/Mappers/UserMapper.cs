// ShortStuff.Data
// UserMapper.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using ShortStuff.Data.Entities;

namespace ShortStuff.Data.Mappers
{
    internal class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            // Table Mapping
            ToTable("Users");

            // Primary Key
            HasKey(u => u.Id);
            Property(u => u.Id)
                .HasPrecision(21, 0)
                .HasColumnType("Numeric")
                .IsRequired();

            // Properties
            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(140);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            Property(u => u.Tag)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(140)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Tag")
                {
                    IsUnique = true
                }));

            Property(u => u.Picture)
                .IsOptional();

            // Relationships
            HasMany(u => u.Followers)
                .WithMany(u => u.Following)
                .Map(u =>
                {
                    u.ToTable("Followers");
                    u.MapLeftKey("FollowerId");
                    u.MapRightKey("FollowingId");
                });

            HasMany(u => u.Favorites)
                .WithMany()
                .Map(u => u.MapLeftKey("MessageId"));

            HasMany(u => u.SubscribedTopics)
                .WithMany(t => t.Subscribers)
                .Map(u =>
                {
                    u.ToTable("TopicSubscribers");
                    u.MapLeftKey("UserId");
                    u.MapRightKey("TopicId");
                });
        }
    }
}
