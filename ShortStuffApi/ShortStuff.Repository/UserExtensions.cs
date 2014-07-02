using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using ShortStuff.Data;
using ShortStuff.Domain.Entities;
using ShortStuff.Repository.ValueInjecter;

namespace ShortStuff.Repository
{
    public static class UserExtensions
    {
        private static bool _withFollowers = false;
        private static bool _withFavorites = false;
        private static bool _withMessages = false;
        private static bool _withNotifications = false;
        private static bool _withSubscribedTopics = false;

        private static void Reset()
        {
            _withFavorites = false;
            _withFollowers = false;
            _withMessages = false;
            _withNotifications = false;
            _withSubscribedTopics = false;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithFollowers(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users)
        {
            _withFollowers = true;
            return users;
        }

        public static Data.Entities.User WithFollowers(this Data.Entities.User users)
        {
            _withFollowers = true;
            return users;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithFavorites(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users)
        {
            _withFavorites = true;
            return users;
        }
        public static Data.Entities.User WithFavorites(this Data.Entities.User users)
        {
            _withFavorites = true;
            return users;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithMessages(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users)
        {
            _withMessages = true;
            return users;
        }
        public static Data.Entities.User WithMessages(this Data.Entities.User users)
        {
            _withMessages = true;
            return users;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithNotifications(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users)
        {
            _withNotifications = true;
            return users;
        }
        public static Data.Entities.User WithNotifications(this Data.Entities.User users)
        {
            _withNotifications = true;
            return users;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithSubscribedTopics(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users)
        {
            _withSubscribedTopics = true;
            return users;
        }
        public static Data.Entities.User WithSubscribedTopics(this Data.Entities.User users)
        {
            _withSubscribedTopics = true;
            return users;
        }

        public static IEnumerable<User> BuildUser(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> dbUser)
        {
            if (dbUser == null)
                return null;

            var user = dbUser.ToList().Select(u => new User
            {
                Id = u.Id,
                Email = u.Email,
                Picture = u.Picture,
                Tag = u.Tag,
                Name = u.Name,
                Followers = _withFollowers ? u.Followers.Select(uu => new User().InjectFrom<SmartConventionInjection>(uu)).Cast<User>() : null,
                Favorites = _withFavorites ? u.Favorites.Select(uf => new Message().InjectFrom<SmartConventionInjection>(uf)).Cast<Message>() : null,
                Messages = _withMessages ? u.Messages.Select(um => new Message().InjectFrom<SmartConventionInjection>(um)).Cast<Message>() : null,
                Notifications = _withNotifications ? u.Notifications.Select(un => new Notification().InjectFrom<SmartConventionInjection>(un)).Cast<Notification>() : null,
                SubscribedTopics = _withSubscribedTopics ? u.SubscribedTopics.Select(ut => new Topic().InjectFrom<SmartConventionInjection>(ut)).Cast<Topic>() : null
            });

            Reset();

            return user;
        }

        public static User BuildUser(this Data.Entities.User dbUser)
        {
            if (dbUser == null)
                return null;

            var ignoredProperties = new List<string>();

            if( !_withFavorites) ignoredProperties.Add("Favorites");
            if (!_withFollowers) ignoredProperties.Add("Followers");
            if (!_withMessages) ignoredProperties.Add("Messages");
            if (!_withNotifications) ignoredProperties.Add("Notifications");
            if (!_withSubscribedTopics) ignoredProperties.Add("SubscribedTopics");

            var user = new User();
            user.InjectFrom(new IgnorePropertyInjection(ignoredProperties), dbUser);

            Reset();

            return user;
        }

    }
}
