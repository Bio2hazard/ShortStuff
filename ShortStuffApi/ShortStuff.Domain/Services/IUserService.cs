// ShortStuff.Domain
// IUserService.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain.Services
{
    public interface IUserService
    {
        ActionResult<User, decimal> GetAll();
        Task<ActionResult<User, decimal>> GetAllAsync();
        ActionResult<User, decimal> GetById(decimal id);
        Task<ActionResult<User, decimal>> GetByIdAsync(decimal id);
        ActionResult<User, decimal> Create(User entity);
        Task<ActionResult<User, decimal>> CreateAsync(User entity);
        ActionResult<User, decimal> Update(User entity);
        Task<ActionResult<User, decimal>> UpdateAsync(User entity);
        ActionResult<User, decimal> Delete(decimal id);
        Task<ActionResult<User, decimal>> DeleteAsync(decimal id);

        ActionResult<User, decimal> GetByTag(string tag);
        Task<ActionResult<User, decimal>> GetByTagAsync(string tag);
    }
}
