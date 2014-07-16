// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationStatus.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   Enumerable listing the status of Notifications.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Enums
{
    /// <summary>
    /// Enumerable listing the status of Notifications.
    /// </summary>
    public enum NotificationStatus
    {
        /// <summary>
        /// The notification has not yet been read by the recipient.
        /// </summary>
        Unread, 

        /// <summary>
        /// The notification has been read by the recipient.
        /// </summary>
        Read
    }
}