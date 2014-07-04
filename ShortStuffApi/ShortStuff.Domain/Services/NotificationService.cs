using System.Collections;
using System.Collections.Generic;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}