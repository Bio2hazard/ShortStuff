// ShortStuff.Domain
// MessageService.cs
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
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Message> GetAll()
        {
            return _unitOfWork.MessageRepository.GetAll();
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _unitOfWork.MessageRepository.GetAllAsync();
        }

        public Message GetById(int id)
        {
            return _unitOfWork.MessageRepository.GetById(id);
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _unitOfWork.MessageRepository.GetByIdAsync(id);
        }

        public CreateStatus<int> Create(Message entity)
        {
            return _unitOfWork.MessageRepository.Create(entity);
        }

        public async Task<CreateStatus<int>> CreateAsync(Message entity)
        {
            return await _unitOfWork.MessageRepository.CreateAsync(entity);
        }

        public UpdateStatus Update(Message entity)
        {
            return _unitOfWork.MessageRepository.Update(entity);
        }

        public async Task<UpdateStatus> UpdateAsync(Message entity)
        {
            return await _unitOfWork.MessageRepository.UpdateAsync(entity);
        }

        public void Delete(int id)
        {
            _unitOfWork.MessageRepository.Delete(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.MessageRepository.DeleteAsync(id);
        }
    }
}
