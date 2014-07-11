// ShortStuff.Domain
// IUserService.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Collections.Generic;
using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        Task<IEnumerable<User>> GetAllAsync();
        User GetById(decimal id);
        Task<User> GetByIdAsync(decimal id);
        CreateStatus<decimal> Create(User entity);
        Task<CreateStatus<decimal>> CreateAsync(User entity);
        UpdateStatus Update(User entity);
        Task<UpdateStatus> UpdateAsync(User entity);
        void Delete(decimal id);
        Task DeleteAsync(decimal id);

        User GetByTag(string tag);
        Task<User> GetByTagAsync(string tag);
    }
}
