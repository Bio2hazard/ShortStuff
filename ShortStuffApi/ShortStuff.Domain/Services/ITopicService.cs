// ShortStuff.Domain
// ITopicService.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain.Services
{
    public interface ITopicService
    {
        ActionResult<Topic, int> GetAll();
        Task<ActionResult<Topic, int>> GetAllAsync();
        ActionResult<Topic, int> GetById(int id);
        Task<ActionResult<Topic, int>> GetByIdAsync(int id);
        ActionResult<Topic, int> Create(Topic entity);
        Task<ActionResult<Topic, int>> CreateAsync(Topic entity);
        ActionResult<Topic, int> Update(Topic entity);
        Task<ActionResult<Topic, int>> UpdateAsync(Topic entity);
        ActionResult<Topic, int> Delete(int id);
        Task<ActionResult<Topic, int>> DeleteAsync(int id);
    }
}
