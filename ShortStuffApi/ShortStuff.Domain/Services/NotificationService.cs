// ShortStuff.Domain
// NotificationService.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ActionResult<Notification, int> _actionResult = new ActionResult<Notification, int>();
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult<Notification, int> GetAll()
        {
            try
            {
                _actionResult.ActionDataSet = _unitOfWork.NotificationRepository.GetAll();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
#if DEBUG
                _actionResult.ActionException = ex;
#endif
            }
            return _actionResult;
        }

        public async Task<ActionResult<Notification, int>> GetAllAsync()
        {
            try
            {
                _actionResult.ActionDataSet = await _unitOfWork.NotificationRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
#if DEBUG
                _actionResult.ActionException = ex;
#endif
            }
            return _actionResult;
        }

        public ActionResult<Notification, int> GetById(int id)
        {
            try
            {
                _actionResult.ActionData = _unitOfWork.NotificationRepository.GetById(id);
                _actionResult.ActionStatus.Status = _actionResult.ActionData != null ? ActionStatusEnum.Success : ActionStatusEnum.NotFound;
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
#if DEBUG
                _actionResult.ActionException = ex;
#endif
            }
            return _actionResult;
        }

        public async Task<ActionResult<Notification, int>> GetByIdAsync(int id)
        {
            try
            {
                _actionResult.ActionData = await _unitOfWork.NotificationRepository.GetByIdAsync(id);
                _actionResult.ActionStatus.Status = _actionResult.ActionData != null ? ActionStatusEnum.Success : ActionStatusEnum.NotFound;
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
#if DEBUG
                _actionResult.ActionException = ex;
#endif
            }
            return _actionResult;
        }

        public ActionResult<Notification, int> Create(Notification entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.Validate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = _unitOfWork.NotificationRepository.Create(entity);
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
#if DEBUG
                _actionResult.ActionException = ex;
#endif
            }
            return _actionResult;
        }

        public async Task<ActionResult<Notification, int>> CreateAsync(Notification entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.Validate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = await _unitOfWork.NotificationRepository.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
#if DEBUG
                _actionResult.ActionException = ex;
#endif
            }
            return _actionResult;
        }

        public ActionResult<Notification, int> Update(Notification entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.ValidateUpdate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = _unitOfWork.NotificationRepository.Update(entity);

                if (_actionResult.ActionStatus.Status == ActionStatusEnum.NotFound)
                {
                    return Create(entity);
                }
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
#if DEBUG
                _actionResult.ActionException = ex;
#endif
            }
            return _actionResult;
        }

        public async Task<ActionResult<Notification, int>> UpdateAsync(Notification entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.ValidateUpdate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = await _unitOfWork.NotificationRepository.UpdateAsync(entity);

                if (_actionResult.ActionStatus.Status == ActionStatusEnum.NotFound)
                {
                    return await CreateAsync(entity);
                }
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
#if DEBUG
                _actionResult.ActionException = ex;
#endif
            }
            return _actionResult;
        }

        public ActionResult<Notification, int> Delete(int id)
        {
            try
            {
                _unitOfWork.NotificationRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
#if DEBUG
                _actionResult.ActionException = ex;
#endif
            }
            return _actionResult;
        }

        public async Task<ActionResult<Notification, int>> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.NotificationRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
#if DEBUG
                _actionResult.ActionException = ex;
#endif
            }
            return _actionResult;
        }
    }
}
