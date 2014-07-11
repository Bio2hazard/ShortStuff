// ShortStuff.Domain
// NotificationType.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

namespace ShortStuff.Domain.Enums
{
    public enum NotificationType
    {
        NewFollower,
        LostFollower,
        FollowedNewMessage,
        FollowedNewEcho,
        NewMention,
        TopicNewMessage
        // Later this could contain private message notifications
    }
}
