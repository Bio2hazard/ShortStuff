using System;
using System.Collections.Generic;
using System.Linq;
using ShortStuff.Data.Entities;

namespace ShortStuff.Data
{
    class ShortStuffDataSeeder
    {
        ShortStuffContext _context;

        public ShortStuffDataSeeder(ShortStuffContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            try
            {
                // Create dummy users
                if (!_context.Users.Any())
                {
                    foreach (var userString in UserSeedData)
                    {
                        var user = new User
                        {
                            Id = userString.UserGoogleId,
                            Name = userString.Name,
                            Email = userString.Email,
                            Tag = userString.Tag,
                            Picture = userString.Picture
                        };

                        _context.Users.Add(user);
                        _context.SaveChanges();
                    }

                    // Create dummy followers
                    foreach (var followerSeed in FollowerSeedData)
                    {
                        var sourceUser = _context.Users.Single(u => u.Id == followerSeed.SourceUserGoogleId);
                        _context.Users.Single(u => u.Id == followerSeed.UserGoogleIdToFollow).Followers.Add(sourceUser);
                        _context.SaveChanges();
                    }
                }
                
                // Create dummy topics
                if (!_context.Topics.Any())
                {
                    foreach (var topicName in _topicSeedData)
                    {
                        var topic = new Topic
                        {
                            Name = topicName
                        };

                        _context.Topics.Add(topic);
                        _context.SaveChanges();
                    }
                }
                

                // Create dummy messages
                if (!_context.Messages.Any())
                {
                    foreach (var messageSeed in MessageSeedData)
                    {
                        var user = _context.Users.Single(u => u.Id == messageSeed.UserGoogleId);
                        Message parentMessage = null;
                        if (messageSeed.ParentMessageId > 0)
                        {
                            parentMessage = _context.Messages.Single(m => m.Id == messageSeed.ParentMessageId);
                        }

                        var message = new Message
                        {
                            Creator = user,
                            CreationDate = DateTime.UtcNow,
                            MessageContent = messageSeed.MessageBody,
                            ParentMessage = parentMessage
                        };

                        _context.Messages.Add(message);

                        // Add message to topic ( if it has a topic )
                        if (messageSeed.TopicId > 0)
                        {
                            var topic = _context.Topics.Single(t => t.Id == messageSeed.TopicId);
                            topic.Messages.Add(message);
                        }
                        _context.SaveChanges();
                    }
                    

                    // Create dummy favorite data
                    foreach (var favoriteSeed in FavoriteSeedData)
                    {
                        var message = _context.Messages.Single(m => m.Id == favoriteSeed.MessageId);
                        _context.Users.Single(u => u.Id == favoriteSeed.SourceUserGoogleId).Favorites.Add(message);
                        _context.SaveChanges();
                    }
                }

                // Create dummy echo data
                if (!_context.Echoes.Any())
                {
                    foreach (var echoSeed in EchoSeedData)
                    {
                        var message = _context.Messages.Single(m => m.Id == echoSeed.MessageIdToEcho);
                        var user = _context.Users.Single(u => u.Id == echoSeed.SourceUserGoogleId);
                        var echo = new Echo
                        {
                            Creator = user,
                            CreationDate = DateTime.UtcNow,
                            SourceMessage = message
                        };

                        _context.Echoes.Add(echo);
                        _context.SaveChanges();
                    }
                    
                }

                // Create dummy notification data
                if (!_context.Notifications.Any())
                {
                    foreach (var notificationSeed in NotificationSeedData)
                    {

                        User user = null;
                        if (notificationSeed.SourceUserId != null)
                        {
                            user = _context.Users.Single(u => u.Id == notificationSeed.SourceUserId);
                        }

                        Message message = null;
                        if (notificationSeed.SourceMessageId > 0)
                        {
                            message = _context.Messages.Single(m => m.Id == notificationSeed.SourceMessageId);
                        }

                        Topic topic = null;
                        if (notificationSeed.SourceTopicId > 0)
                        {
                            topic = _context.Topics.Single(t => t.Id == notificationSeed.SourceTopicId);
                        }

                        var ownerUser = _context.Users.Single(u => u.Id == notificationSeed.OwnerGoogleId);

                        var notification = new Notification
                        {
                            Owner = ownerUser,
                            CreationDate = DateTime.UtcNow,
                            NotificationType = (int)notificationSeed.NotificationType,
                            SourceMessage = message,
                            SourceUser = user,
                            SourceTopic = topic,
                            NotificationStatus = (int)notificationSeed.NotificationStatus
                        };

                        _context.Notifications.Add(notification);
                        _context.SaveChanges();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static IList<UserSeed> UserSeedData = new List<UserSeed>
        {
            new UserSeed {UserGoogleId = 1, Name = "John Doe", Email = "johndoe@gmail.com", Tag = "johndoe", Picture = "picturejohn"},
            new UserSeed {UserGoogleId = 2, Name = "Jane Doe", Email = "janedoe@gmail.com", Tag = "janedoe", Picture = "picturejane"},
            new UserSeed {UserGoogleId = 3, Name = "Messer Schmidt", Email = "messer@gmail.com", Tag = "messer", Picture = "picturemesser"},
            new UserSeed {UserGoogleId = 4, Name = "Hans Stur", Email = "hans@gmail.com", Tag = "hansmans", Picture = "picturehans"},
        }; 

        private static string[] _topicSeedData =
        {
            "#dummiesunite"
        };

        private static IList<MessageSeed> MessageSeedData = new List<MessageSeed>
        {
            new MessageSeed {UserGoogleId = 1, MessageBody = "My first ShortMessage"},
            new MessageSeed {UserGoogleId = 1, MessageBody = "@janedoe are you getting this?"},
            new MessageSeed {UserGoogleId = 2, MessageBody = "@johndoe reading you loud and clear", ParentMessageId = 2},
            new MessageSeed {UserGoogleId = 3, MessageBody = "Testing 123"},
            new MessageSeed {UserGoogleId = 3, MessageBody = "#dummiesunite How are all ya fellow dummies doin?", TopicId = 1},
            new MessageSeed {UserGoogleId = 4, MessageBody = "#dummiesunite Wonderful!", TopicId = 1}

        };

        private static IList<FollowerSeed> FollowerSeedData = new List<FollowerSeed>
        {
            new FollowerSeed {SourceUserGoogleId = 2, UserGoogleIdToFollow = 1},
            new FollowerSeed {SourceUserGoogleId = 1, UserGoogleIdToFollow = 2}
        };

        private static IList<FavoriteSeed> FavoriteSeedData = new List<FavoriteSeed>
        {
            new FavoriteSeed {MessageId = 1, SourceUserGoogleId = 2},
            new FavoriteSeed {MessageId = 4, SourceUserGoogleId = 1}
        };

        private static IList<EchoSeed> EchoSeedData = new List<EchoSeed>
        {
            new EchoSeed {SourceUserGoogleId = 4, MessageIdToEcho = 1}
        }; 

        private static IList<NotificationSeed> NotificationSeedData = new List<NotificationSeed>
        {
            new NotificationSeed {OwnerGoogleId = 1, NotificationType = NotificationType.NewFollower, SourceUserId = 2, NotificationStatus = NotificationStatus.Unread},
            new NotificationSeed {OwnerGoogleId = 2, NotificationType = NotificationType.FollowedNewMessage, SourceUserId = 2,SourceMessageId = 2, NotificationStatus = NotificationStatus.Unread},
            new NotificationSeed {OwnerGoogleId = 4, NotificationType = NotificationType.TopicNewMessage, SourceUserId = 3,SourceTopicId = 1, SourceMessageId = 5, NotificationStatus = NotificationStatus.Unread}
        }; 

        class UserSeed
        {
            public decimal UserGoogleId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Tag { get; set; }
            public string Picture { get; set; }
        }

        class MessageSeed
        {
            public decimal UserGoogleId { get; set; }
            public string MessageBody { get; set; }
            public int ParentMessageId { get; set; }
            public int TopicId { get; set; }
        }

        class FollowerSeed
        {
            public decimal SourceUserGoogleId { get; set; }
            public decimal UserGoogleIdToFollow { get; set; }
        }

        class EchoSeed
        {
            public decimal SourceUserGoogleId { get; set; }
            public int MessageIdToEcho { get; set; }
        }

        class FavoriteSeed
        {
            public decimal SourceUserGoogleId { get; set; }
            public int MessageId { get; set; }
        }

        class NotificationSeed
        {
            public decimal OwnerGoogleId { get; set; }
            public NotificationType NotificationType { get; set; }
            public int SourceMessageId { get; set; }
            public decimal SourceUserId { get; set; }
            public int SourceTopicId { get; set; }
            public NotificationStatus NotificationStatus { get; set; }
        }

        private enum NotificationType
        {
            NewFollower = 0,
            LostFollower = 1,
            FollowedNewMessage = 2,
            FollowedNewEcho = 3,
            NewMention = 4,
            TopicNewMessage = 5
            // Later this could contain private message notifications
        }
        private enum NotificationStatus
        {
            Unread = 0,
            Read = 1
        }

    }
}
