// ShortStuff.Domain
// MessageService.cs
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
    public class MessageService : IMessageService
    {
        private readonly ActionResult<Message, int> _actionResult = new ActionResult<Message, int>();
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult<Message, int> GetAll()
        {
            try
            {
                _actionResult.ActionDataSet = _unitOfWork.MessageRepository.GetAll();
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

        public async Task<ActionResult<Message, int>> GetAllAsync()
        {
            try
            {
                _actionResult.ActionDataSet = await _unitOfWork.MessageRepository.GetAllAsync();
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

        public ActionResult<Message, int> GetById(int id)
        {
            try
            {
                _actionResult.ActionData = _unitOfWork.MessageRepository.GetById(id);
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

        public async Task<ActionResult<Message, int>> GetByIdAsync(int id)
        {
            try
            {
                _actionResult.ActionData = await _unitOfWork.MessageRepository.GetByIdAsync(id);
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

        public ActionResult<Message, int> Create(Message entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.Validate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = _unitOfWork.MessageRepository.Create(entity);
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

        public async Task<ActionResult<Message, int>> CreateAsync(Message entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.Validate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = await _unitOfWork.MessageRepository.CreateAsync(entity);
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

        public ActionResult<Message, int> Update(Message entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.ValidateUpdate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = _unitOfWork.MessageRepository.Update(entity);

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

        public async Task<ActionResult<Message, int>> UpdateAsync(Message entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.ValidateUpdate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = await _unitOfWork.MessageRepository.UpdateAsync(entity);

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

        public ActionResult<Message, int> Delete(int id)
        {
            try
            {
                _unitOfWork.MessageRepository.Delete(id);
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

        public async Task<ActionResult<Message, int>> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.MessageRepository.DeleteAsync(id);
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
