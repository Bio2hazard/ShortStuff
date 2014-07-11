// ShortStuff.Domain
// EchoService.cs
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
    public class EchoService : IEchoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EchoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Echo> GetAll()
        {
            return _unitOfWork.EchoRepository.GetAll();
        }

        public async Task<IEnumerable<Echo>> GetAllAsync()
        {
            return await _unitOfWork.EchoRepository.GetAllAsync();
        }

        public Echo GetById(int id)
        {
            return _unitOfWork.EchoRepository.GetById(id);
        }

        public async Task<Echo> GetByIdAsync(int id)
        {
            return await _unitOfWork.EchoRepository.GetByIdAsync(id);
        }

        public CreateStatus<int> Create(Echo entity)
        {
            return _unitOfWork.EchoRepository.Create(entity);
        }

        public async Task<CreateStatus<int>> CreateAsync(Echo entity)
        {
            return await _unitOfWork.EchoRepository.CreateAsync(entity);
        }

        public UpdateStatus Update(Echo entity)
        {
            return _unitOfWork.EchoRepository.Update(entity);
        }

        public async Task<UpdateStatus> UpdateAsync(Echo entity)
        {
            return await _unitOfWork.EchoRepository.UpdateAsync(entity);
        }

        public void Delete(int id)
        {
            _unitOfWork.EchoRepository.Delete(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.EchoRepository.DeleteAsync(id);
        }
    }
}
