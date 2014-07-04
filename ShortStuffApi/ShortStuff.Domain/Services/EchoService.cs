using System.Collections;
using System.Collections.Generic;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Domain.Services
{
    public class EchoService : IEchoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EchoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}