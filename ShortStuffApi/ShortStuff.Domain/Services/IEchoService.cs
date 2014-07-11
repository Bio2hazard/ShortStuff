// ShortStuff.Domain
// IEchoService.cs
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
    public interface IEchoService
    {
        IEnumerable<Echo> GetAll();
        Task<IEnumerable<Echo>> GetAllAsync();
        Echo GetById(int id);
        Task<Echo> GetByIdAsync(int id);
        CreateStatus<int> Create(Echo entity);
        Task<CreateStatus<int>> CreateAsync(Echo entity);
        UpdateStatus Update(Echo entity);
        Task<UpdateStatus> UpdateAsync(Echo entity);
        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
