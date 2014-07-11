// ShortStuff.Domain
// INotificationService.cs
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
    public interface INotificationService
    {
        IEnumerable<Notification> GetAll();
        Task<IEnumerable<Notification>> GetAllAsync();
        Notification GetById(int id);
        Task<Notification> GetByIdAsync(int id);
        CreateStatus<int> Create(Notification entity);
        Task<CreateStatus<int>> CreateAsync(Notification entity);
        UpdateStatus Update(Notification entity);
        Task<UpdateStatus> UpdateAsync(Notification entity);
        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
