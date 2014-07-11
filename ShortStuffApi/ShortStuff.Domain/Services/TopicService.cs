// ShortStuff.Domain
// TopicService.cs
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
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Topic> GetAll()
        {
            return _unitOfWork.TopicRepository.GetAll();
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _unitOfWork.TopicRepository.GetAllAsync();
        }

        public Topic GetById(int id)
        {
            return _unitOfWork.TopicRepository.GetById(id);
        }

        public async Task<Topic> GetByIdAsync(int id)
        {
            return await _unitOfWork.TopicRepository.GetByIdAsync(id);
        }

        public CreateStatus<int> Create(Topic entity)
        {
            return _unitOfWork.TopicRepository.Create(entity);
        }

        public async Task<CreateStatus<int>> CreateAsync(Topic entity)
        {
            return await _unitOfWork.TopicRepository.CreateAsync(entity);
        }

        public UpdateStatus Update(Topic entity)
        {
            return _unitOfWork.TopicRepository.Update(entity);
        }

        public async Task<UpdateStatus> UpdateAsync(Topic entity)
        {
            return await _unitOfWork.TopicRepository.UpdateAsync(entity);
        }

        public void Delete(int id)
        {
            _unitOfWork.TopicRepository.Delete(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.TopicRepository.DeleteAsync(id);
        }
    }
}
