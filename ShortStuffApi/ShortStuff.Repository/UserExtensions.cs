using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using ShortStuff.Domain.Entities;
using ShortStuff.Repository.ValueInjecter;

namespace ShortStuff.Repository
{
    public static class UserExtensions
    {
        private static bool _withFollowers = false;
        private static int _followersDepth = 0;

        private static bool _withFollowing = false;
        private static int _followingDepth = 0;

        private static bool _withFavorites = false;
        private static int _favoritesDepth = 0;

        private static bool _withMessages = false;
        private static int _messagesDepth = 0;

        private static bool _withNotifications = false;
        private static int _notificationsDepth = 0;

        private static bool _withSubscribedTopics = false;
        private static int _subscribedTopicsDepth = 0;

        private static void Reset()
        {
            _withFavorites = false;
            _favoritesDepth = 0;

            _withFollowers = false;
            _followersDepth = 0;

            _withFollowing = false;
            _followingDepth = 0;

            _withMessages = false;
            _messagesDepth = 0;

            _withNotifications = false;
            _notificationsDepth = 0;

            _withSubscribedTopics = false;
            _subscribedTopicsDepth = 0;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithFollowers(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users, int depth = 0)
        {
            _withFollowers = true;
            _followersDepth = depth;
            return users;
        }

        public static Data.Entities.User WithFollowers(this Data.Entities.User users, int depth = 0)
        {
            _withFollowers = true;
            _followersDepth = depth;
            return users;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithFollowing(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users, int depth = 0)
        {
            _withFollowing = true;
            _followingDepth = depth;
            return users;
        }

        public static Data.Entities.User WithFollowing(this Data.Entities.User users, int depth = 0)
        {
            _withFollowing = true;
            _followingDepth = depth;
            return users;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithFavorites(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users, int depth = 0)
        {
            _withFavorites = true;
            _favoritesDepth = depth;
            return users;
        }
        public static Data.Entities.User WithFavorites(this Data.Entities.User users, int depth = 0)
        {
            _withFavorites = true;
            _favoritesDepth = depth;
            return users;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithMessages(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users, int depth = 0)
        {
            _withMessages = true;
            _messagesDepth = depth;
            return users;
        }
        public static Data.Entities.User WithMessages(this Data.Entities.User users, int depth = 0)
        {
            _withMessages = true;
            _messagesDepth = depth;
            return users;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithNotifications(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users, int depth = 0)
        {
            _withNotifications = true;
            _notificationsDepth = depth;
            return users;
        }
        public static Data.Entities.User WithNotifications(this Data.Entities.User users, int depth = 0)
        {
            _withNotifications = true;
            _notificationsDepth = depth;
            return users;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> WithSubscribedTopics(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.User> users, int depth = 0)
        {
            _withSubscribedTopics = true;
            _subscribedTopicsDepth = depth;
            return users;
        }
        public static Data.Entities.User WithSubscribedTopics(this Data.Entities.User users, int depth = 0)
        {
            _withSubscribedTopics = true;
            _subscribedTopicsDepth = depth;
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
                Followers = _withFollowers ? u.Followers.InjectFromHelper(new User(), _followersDepth) : null,
                Following = _withFollowing ? u.Following.InjectFromHelper(new User(), _followingDepth) : null,
                Favorites = _withFavorites ? u.Favorites.InjectFromHelper(new Message(), _favoritesDepth) : null,
                Messages = _withMessages ? u.Messages.InjectFromHelper(new Message(), _messagesDepth) : null,
                Notifications = _withNotifications ? u.Notifications.InjectFromHelper(new Notification(), _notificationsDepth) : null,
                SubscribedTopics = _withSubscribedTopics ? u.SubscribedTopics.InjectFromHelper(new Topic(), _subscribedTopicsDepth) : null
            }).ToList();

            Reset();

            return user;
        }

        public static User BuildUser(this Data.Entities.User dbUser)
        {
            if (dbUser == null)
                return null;


            var propertyDict = new Dictionary<string, SinglePropertyDepthInjection.PropPair>();

            propertyDict.Add("Favorites", new SinglePropertyDepthInjection.PropPair { Depth = _favoritesDepth, Ignored = !_withFavorites });
            propertyDict.Add("Followers", new SinglePropertyDepthInjection.PropPair { Depth = _followersDepth, Ignored = !_withFollowers });
            propertyDict.Add("Following", new SinglePropertyDepthInjection.PropPair { Depth = _followingDepth, Ignored = !_withFollowing });
            propertyDict.Add("Messages", new SinglePropertyDepthInjection.PropPair { Depth = _messagesDepth, Ignored = !_withMessages });
            propertyDict.Add("Notifications", new SinglePropertyDepthInjection.PropPair { Depth = _notificationsDepth, Ignored = !_withNotifications });
            propertyDict.Add("SubscribedTopics", new SinglePropertyDepthInjection.PropPair { Depth = _subscribedTopicsDepth, Ignored = !_withSubscribedTopics });

            var user = new User();
            user.InjectFrom(new SinglePropertyDepthInjection(propertyDict), dbUser);

            Reset();

            return user;
        }
    }
}