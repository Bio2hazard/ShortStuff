// ShortStuff.Domain
// IEchoService.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain.Services
{
    public interface IEchoService
    {
        ActionResult<Echo, int> GetAll();
        Task<ActionResult<Echo, int>> GetAllAsync();
        ActionResult<Echo, int> GetById(int id);
        Task<ActionResult<Echo, int>> GetByIdAsync(int id);
        ActionResult<Echo, int> Create(Echo entity);
        Task<ActionResult<Echo, int>> CreateAsync(Echo entity);
        ActionResult<Echo, int> Update(Echo entity);
        Task<ActionResult<Echo, int>> UpdateAsync(Echo entity);
        ActionResult<Echo, int> Delete(int id);
        Task<ActionResult<Echo, int>> DeleteAsync(int id);
    }
}
