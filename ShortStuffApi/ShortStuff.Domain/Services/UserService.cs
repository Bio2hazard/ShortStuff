using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<User> GetByTagAsync(string tag)
        {
            return null;
            //return await //_unitOfWork.UserRepository //_context.Users.BuildQueryAsync<Data.Entities.User, User>(u => u.Id == id);
        }
    }
}