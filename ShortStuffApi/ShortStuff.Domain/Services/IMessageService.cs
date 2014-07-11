// ShortStuff.Domain
// IMessageService.cs
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
    public interface IMessageService
    {
        IEnumerable<Message> GetAll();
        Task<IEnumerable<Message>> GetAllAsync();
        Message GetById(int id);
        Task<Message> GetByIdAsync(int id);
        CreateStatus<int> Create(Message entity);
        Task<CreateStatus<int>> CreateAsync(Message entity);
        UpdateStatus Update(Message entity);
        Task<UpdateStatus> UpdateAsync(Message entity);
        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
