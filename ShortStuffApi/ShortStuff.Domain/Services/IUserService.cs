using System.Collections.Generic;
using System.Threading.Tasks;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Domain.Services
{
    public interface IUserService
    {
        Task<User> GetByTagAsync(string tag);
    }
}
