using System.Collections;
using System.Collections.Generic;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Domain.Services
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}