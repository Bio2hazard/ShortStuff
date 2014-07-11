// ShortStuff.Domain
// UserService.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public User GetById(decimal id)
        {
            return _unitOfWork.UserRepository.GetById(id);
        }

        public async Task<User> GetByIdAsync(decimal id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public CreateStatus<decimal> Create(User entity)
        {
            return _unitOfWork.UserRepository.Create(entity);
        }

        public async Task<CreateStatus<decimal>> CreateAsync(User entity)
        {
            return await _unitOfWork.UserRepository.CreateAsync(entity);
        }

        public UpdateStatus Update(User entity)
        {
            return _unitOfWork.UserRepository.Update(entity);
        }

        public async Task<UpdateStatus> UpdateAsync(User entity)
        {
            return await _unitOfWork.UserRepository.UpdateAsync(entity);
        }

        public void Delete(decimal id)
        {
            _unitOfWork.UserRepository.Delete(id);
        }

        public async Task DeleteAsync(decimal id)
        {
            await _unitOfWork.UserRepository.DeleteAsync(id);
        }

        public User GetByTag(string tag)
        {
            return _unitOfWork.UserRepository.GetOne(u => u.Tag, tag, ExpressionType.Equal);
        }

        public async Task<User> GetByTagAsync(string tag)
        {
            return await _unitOfWork.UserRepository.GetOneAsync(u => u.Tag, tag, ExpressionType.Equal);
        }
    }
}
