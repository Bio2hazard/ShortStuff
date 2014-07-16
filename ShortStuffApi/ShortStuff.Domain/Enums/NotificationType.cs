// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationType.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   Enumerable listing the possible Types of Notifications.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Enums
{
    /// <summary>
    /// Enumerable listing the possible Types of Notifications.
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// Notification for a user that has gained a Follower.
        /// </summary>
        NewFollower, 

        /// <summary>
        /// Notification for a user that has lost a follower.
        /// </summary>
        LostFollower, 

        /// <summary>
        /// Notification for a user following someone, who has posted a new message.
        /// </summary>
        FollowedNewMessage, 

        /// <summary>
        /// Notification for a user following someone, who has posted a new echo.
        /// </summary>
        FollowedNewEcho, 

        /// <summary>
        /// Notification for a user who was specifically mentioned in a message.
        /// </summary>
        NewMention, 

        /// <summary>
        /// Notification for a user who has subscribed to a topic, in which a new post was created.
        /// </summary>
        TopicNewMessage

        // Later this could contain private message notifications
    }
}