// ShortStuff.Domain
// NotificationService.cs
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
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Notification> GetAll()
        {
            return _unitOfWork.NotificationRepository.GetAll();
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _unitOfWork.NotificationRepository.GetAllAsync();
        }

        public Notification GetById(int id)
        {
            return _unitOfWork.NotificationRepository.GetById(id);
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _unitOfWork.NotificationRepository.GetByIdAsync(id);
        }

        public CreateStatus<int> Create(Notification entity)
        {
            return _unitOfWork.NotificationRepository.Create(entity);
        }

        public async Task<CreateStatus<int>> CreateAsync(Notification entity)
        {
            return await _unitOfWork.NotificationRepository.CreateAsync(entity);
        }

        public UpdateStatus Update(Notification entity)
        {
            return _unitOfWork.NotificationRepository.Update(entity);
        }

        public async Task<UpdateStatus> UpdateAsync(Notification entity)
        {
            return await _unitOfWork.NotificationRepository.UpdateAsync(entity);
        }

        public void Delete(int id)
        {
            _unitOfWork.NotificationRepository.Delete(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.NotificationRepository.DeleteAsync(id);
        }
    }
}
