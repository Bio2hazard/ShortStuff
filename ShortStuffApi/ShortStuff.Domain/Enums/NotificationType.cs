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
