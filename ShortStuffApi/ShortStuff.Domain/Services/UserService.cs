using System.Collections;
using System.Collections.Generic;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}