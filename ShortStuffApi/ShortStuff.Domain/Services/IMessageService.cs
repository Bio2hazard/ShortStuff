// ShortStuff.Domain
// IMessageService.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain.Services
{
    public interface IMessageService
    {
        ActionResult<Message, int> GetAll();
        Task<ActionResult<Message, int>> GetAllAsync();
        ActionResult<Message, int> GetById(int id);
        Task<ActionResult<Message, int>> GetByIdAsync(int id);
        ActionResult<Message, int> Create(Message entity);
        Task<ActionResult<Message, int>> CreateAsync(Message entity);
        ActionResult<Message, int> Update(Message entity);
        Task<ActionResult<Message, int>> UpdateAsync(Message entity);
        ActionResult<Message, int> Delete(int id);
        Task<ActionResult<Message, int>> DeleteAsync(int id);
    }
}
