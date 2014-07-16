// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShortStuffDataSeeder.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The short stuff data seeder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ShortStuff.Data.Entities;

    /// <summary>
    /// The short stuff data seeder.
    /// </summary>
    internal class ShortStuffDataSeeder
    {
        /// <summary>
        /// The user seed data.
        /// </summary>
        private static readonly IList<UserSeed> UserSeedData = new List<UserSeed>
                                                                   {
                                                                       new UserSeed
                                                                           {
                                                                               UserGoogleId = 1, 
                                                                               Name = "John Doe", 
                                                                               Email = "johndoe@gmail.com", 
                                                                               Tag = "johndoe", 
                                                                               Picture = "picturejohn"
                                                                           }, 
                                                                       new UserSeed
                                                                           {
                                                                               UserGoogleId = 2, 
                                                                               Name = "Jane Doe", 
                                                                               Email = "janedoe@gmail.com", 
                                                                               Tag = "janedoe", 
                                                                               Picture = "picturejane"
                                                                           }, 
                                                                       new UserSeed
                                                                           {
                                                                               UserGoogleId = 3, 
                                                                               Name = "Messer Schmidt", 
                                                                               Email = "messer@gmail.com", 
                                                                               Tag = "messer", 
                                                                               Picture = "picturemesser"
                                                                           }, 
                                                                       new UserSeed
                                                                           {
                                                                               UserGoogleId = 4, 
                                                                               Name = "Hans Stur", 
                                                                               Email = "hans@gmail.com", 
                                                                               Tag = "hansmans", 
                                                                               Picture = "picturehans"
                                                                           }, 
                                                                   };

        /// <summary>
        /// The topic seed data.
        /// </summary>
        private static readonly string[] TopicSeedData = { "#dummiesunite" };

        /// <summary>
        /// The message seed data.
        /// </summary>
        private static readonly IList<MessageSeed> MessageSeedData = new List<MessageSeed>
                                                                         {
                                                                             new MessageSeed { UserGoogleId = 1, MessageBody = "My first ShortMessage" }, 
                                                                             new MessageSeed { UserGoogleId = 1, MessageBody = "@janedoe are you getting this?" }, 
                                                                             new MessageSeed
                                                                                 {
                                                                                     UserGoogleId = 2, 
                                                                                     MessageBody = "@johndoe reading you loud and clear", 
                                                                                     ParentMessageId = 2
                                                                                 }, 
                                                                             new MessageSeed { UserGoogleId = 3, MessageBody = "Testing 123" }, 
                                                                             new MessageSeed
                                                                                 {
                                                                                     UserGoogleId = 3, 
                                                                                     MessageBody = "#dummiesunite How are all ya fellow dummies doin?", 
                                                                                     TopicId = 1
                                                                                 }, 
                                                                             new MessageSeed
                                                                                 {
                                                                                     UserGoogleId = 4, 
                                                                                     MessageBody = "#dummiesunite Wonderful!", 
                                                                                     TopicId = 1
                                                                                 }
                                                                         };

        /// <summary>
        /// The follower seed data.
        /// </summary>
        private static readonly IList<FollowerSeed> FollowerSeedData = new List<FollowerSeed>
                                                                           {
                                                                               new FollowerSeed { SourceUserGoogleId = 2, UserGoogleIdToFollow = 1 }, 
                                                                               new FollowerSeed { SourceUserGoogleId = 1, UserGoogleIdToFollow = 2 }
                                                                           };

        /// <summary>
        /// The favorite seed data.
        /// </summary>
        private static readonly IList<FavoriteSeed> FavoriteSeedData = new List<FavoriteSeed>
                                                                           {
                                                                               new FavoriteSeed { MessageId = 1, SourceUserGoogleId = 2 }, 
                                                                               new FavoriteSeed { MessageId = 4, SourceUserGoogleId = 1 }
                                                                           };

        /// <summary>
        /// The echo seed data.
        /// </summary>
        private static readonly IList<EchoSeed> EchoSeedData = new List<EchoSeed> { new EchoSeed { SourceUserGoogleId = 4, MessageIdToEcho = 1 } };

        /// <summary>
        /// The notification seed data.
        /// </summary>
        private static readonly IList<NotificationSeed> NotificationSeedData = new List<NotificationSeed>
                                                                                   {
                                                                                       new NotificationSeed
                                                                                           {
                                                                                               OwnerGoogleId = 1, 
                                                                                               NotificationType = NotificationType.NewFollower, 
                                                                                               SourceUserId = 2, 
                                                                                               NotificationStatus = NotificationStatus.Unread
                                                                                           }, 
                                                                                       new NotificationSeed
                                                                                           {
                                                                                               OwnerGoogleId = 2, 
                                                                                               NotificationType =
                                                                                                   NotificationType.FollowedNewMessage, 
                                                                                               SourceUserId = 2, 
                                                                                               SourceMessageId = 2, 
                                                                                               NotificationStatus = NotificationStatus.Unread
                                                                                           }, 
                                                                                       new NotificationSeed
                                                                                           {
                                                                                               OwnerGoogleId = 4, 
                                                                                               NotificationType =
                                                                                                   NotificationType.TopicNewMessage, 
                                                                                               SourceUserId = 3, 
                                                                                               SourceTopicId = 1, 
                                                                                               SourceMessageId = 5, 
                                                                                               NotificationStatus = NotificationStatus.Unread
                                                                                           }
                                                                                   };

        /// <summary>
        /// The _context.
        /// </summary>
        private readonly ShortStuffContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortStuffDataSeeder"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public ShortStuffDataSeeder(ShortStuffContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The notification status.
        /// </summary>
        private enum NotificationStatus
        {
            /// <summary>
            /// The unread.
            /// </summary>
            Unread = 0, 

            /// <summary>
            /// The read.
            /// </summary>
            Read = 1
        }

        /// <summary>
        /// The notification type.
        /// </summary>
        private enum NotificationType
        {
            /// <summary>
            /// The new follower.
            /// </summary>
            NewFollower = 0, 

            /// <summary>
            /// The lost follower.
            /// </summary>
            LostFollower = 1, 

            /// <summary>
            /// The followed new message.
            /// </summary>
            FollowedNewMessage = 2, 

            /// <summary>
            /// The followed new echo.
            /// </summary>
            FollowedNewEcho = 3, 

            /// <summary>
            /// The new mention.
            /// </summary>
            NewMention = 4, 

            /// <summary>
            /// The topic new message.
            /// </summary>
            TopicNewMessage = 5

            // Later this could contain private message notifications
        }

        /// <summary>
        /// The seed.
        /// </summary>
        public void Seed()
        {
            // Create dummy users
            if (!_context.Users.Any())
            {
                foreach (var userString in UserSeedData)
                {
                    var user = new User { Id = userString.UserGoogleId, Name = userString.Name, Email = userString.Email, Tag = userString.Tag, Picture = userString.Picture };

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
                foreach (var topicName in TopicSeedData)
                {
                    var topic = new Topic { Name = topicName };

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

                    var message = new Message { Creator = user, CreationDate = DateTime.UtcNow, MessageContent = messageSeed.MessageBody, ParentMessage = parentMessage };

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
                    var echo = new Echo { Creator = user, CreationDate = DateTime.UtcNow, SourceMessage = message };

                    _context.Echoes.Add(echo);
                    _context.SaveChanges();
                }
            }

            // Create dummy notification data
            if (!_context.Notifications.Any())
            {
                foreach (var notificationSeed in NotificationSeedData)
                {
                    var user = _context.Users.Single(u => u.Id == notificationSeed.SourceUserId);

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

        /// <summary>
        /// The echo seed.
        /// </summary>
        private class EchoSeed
        {
            /// <summary>
            /// Gets or sets the source user google id.
            /// </summary>
            public decimal SourceUserGoogleId { get; set; }

            /// <summary>
            /// Gets or sets the message id to echo.
            /// </summary>
            public int MessageIdToEcho { get; set; }
        }

        /// <summary>
        /// The favorite seed.
        /// </summary>
        private class FavoriteSeed
        {
            /// <summary>
            /// Gets or sets the source user google id.
            /// </summary>
            public decimal SourceUserGoogleId { get; set; }

            /// <summary>
            /// Gets or sets the message id.
            /// </summary>
            public int MessageId { get; set; }
        }

        /// <summary>
        /// The follower seed.
        /// </summary>
        private class FollowerSeed
        {
            /// <summary>
            /// Gets or sets the source user google id.
            /// </summary>
            public decimal SourceUserGoogleId { get; set; }

            /// <summary>
            /// Gets or sets the user google id to follow.
            /// </summary>
            public decimal UserGoogleIdToFollow { get; set; }
        }

        /// <summary>
        /// The message seed.
        /// </summary>
        private class MessageSeed
        {
            /// <summary>
            /// Gets or sets the user google id.
            /// </summary>
            public decimal UserGoogleId { get; set; }

            /// <summary>
            /// Gets or sets the message body.
            /// </summary>
            public string MessageBody { get; set; }

            /// <summary>
            /// Gets or sets the parent message id.
            /// </summary>
            public int ParentMessageId { get; set; }

            /// <summary>
            /// Gets or sets the topic id.
            /// </summary>
            public int TopicId { get; set; }
        }

        /// <summary>
        /// The notification seed.
        /// </summary>
        private class NotificationSeed
        {
            /// <summary>
            /// Gets or sets the owner google id.
            /// </summary>
            public decimal OwnerGoogleId { get; set; }

            /// <summary>
            /// Gets or sets the notification type.
            /// </summary>
            public NotificationType NotificationType { get; set; }

            /// <summary>
            /// Gets or sets the source message id.
            /// </summary>
            public int SourceMessageId { get; set; }

            /// <summary>
            /// Gets or sets the source user id.
            /// </summary>
            public decimal SourceUserId { get; set; }

            /// <summary>
            /// Gets or sets the source topic id.
            /// </summary>
            public int SourceTopicId { get; set; }

            /// <summary>
            /// Gets or sets the notification status.
            /// </summary>
            public NotificationStatus NotificationStatus { get; set; }
        }

        // ReSharper disable UnusedMember.Local

        /// <summary>
        /// The user seed.
        /// </summary>
        private class UserSeed
        {
            /// <summary>
            /// Gets or sets the user google id.
            /// </summary>
            public decimal UserGoogleId { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the email.
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the tag.
            /// </summary>
            public string Tag { get; set; }

            /// <summary>
            /// Gets or sets the picture.
            /// </summary>
            public string Picture { get; set; }
        }

        // ReSharper restore UnusedMember.Local
    }
}