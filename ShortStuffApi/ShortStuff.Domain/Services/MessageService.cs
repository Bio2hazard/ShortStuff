using System.Collections;
using System.Collections.Generic;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Domain.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}