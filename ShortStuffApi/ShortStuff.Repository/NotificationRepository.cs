// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationRepository.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The notification repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository
{
    using ShortStuff.Data;
    using ShortStuff.Data.Entities;

    /// <summary>
    /// The notification repository.
    /// </summary>
    public class NotificationRepository : RepositoryBase<Notification, Domain.Entities.Notification, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public NotificationRepository(ShortStuffContext context)
            : base(context)
        {
        }
    }
}