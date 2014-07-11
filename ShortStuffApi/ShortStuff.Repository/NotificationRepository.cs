// ShortStuff.Repository
// NotificationRepository.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using ShortStuff.Data;
using ShortStuff.Data.Entities;

namespace ShortStuff.Repository
{
    public class NotificationRepository : RepositoryBase<Notification, Domain.Entities.Notification, int>
    {
        public NotificationRepository(ShortStuffContext context) : base(context)
        {
        }
    }
}
