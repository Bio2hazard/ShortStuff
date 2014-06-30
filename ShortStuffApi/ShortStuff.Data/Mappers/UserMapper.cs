using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using ShortStuff.Data.Entities;

namespace ShortStuff.Data.Mappers
{
    class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            // Table Mapping
            ToTable("Users");

            // Primary Key
            HasKey(u => u.Id);
            Property(u => u.Id)
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
                .HasMaxLength(255)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));

            Property(u => u.Picture)
                .IsOptional();

            // Relationships
            HasMany(u => u.Followers)
                .WithMany()
                .Map(u => u.MapLeftKey("FollowerID"));

            HasMany(u => u.Favorites)
                .WithMany()
                .Map(u => u.MapLeftKey("MessageID"));

            HasMany(u => u.SubscribedTopics)
                .WithMany(t => t.Subscribers)
                .Map(u =>
                {
                    u.ToTable("TopicSubscribers");
                    u.MapLeftKey("UserID");
                    u.MapRightKey("TopicID");
                });
        }
    }
}
