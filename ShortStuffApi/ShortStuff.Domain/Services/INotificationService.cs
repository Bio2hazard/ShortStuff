// ShortStuff.Domain
// INotificationService.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain.Services
{
    public interface INotificationService
    {
        ActionResult<Notification, int> GetAll();
        Task<ActionResult<Notification, int>> GetAllAsync();
        ActionResult<Notification, int> GetById(int id);
        Task<ActionResult<Notification, int>> GetByIdAsync(int id);
        ActionResult<Notification, int> Create(Notification entity);
        Task<ActionResult<Notification, int>> CreateAsync(Notification entity);
        ActionResult<Notification, int> Update(Notification entity);
        Task<ActionResult<Notification, int>> UpdateAsync(Notification entity);
        ActionResult<Notification, int> Delete(int id);
        Task<ActionResult<Notification, int>> DeleteAsync(int id);
    }
}
