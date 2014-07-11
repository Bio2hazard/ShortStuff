// ShortStuff.Domain
// ITopicService.cs
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
    public interface ITopicService
    {
        IEnumerable<Topic> GetAll();
        Task<IEnumerable<Topic>> GetAllAsync();
        Topic GetById(int id);
        Task<Topic> GetByIdAsync(int id);
        CreateStatus<int> Create(Topic entity);
        Task<CreateStatus<int>> CreateAsync(Topic entity);
        UpdateStatus Update(Topic entity);
        Task<UpdateStatus> UpdateAsync(Topic entity);
        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
