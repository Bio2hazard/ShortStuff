// ShortStuff.Domain
// TopicService.cs
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
    public class TopicService : ITopicService
    {
        private readonly ActionResult<Topic, int> _actionResult = new ActionResult<Topic, int>();
        private readonly IUnitOfWork _unitOfWork;

        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult<Topic, int> GetAll()
        {
            try
            {
                _actionResult.ActionDataSet = _unitOfWork.TopicRepository.GetAll();
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

        public async Task<ActionResult<Topic, int>> GetAllAsync()
        {
            try
            {
                _actionResult.ActionDataSet = await _unitOfWork.TopicRepository.GetAllAsync();
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

        public ActionResult<Topic, int> GetById(int id)
        {
            try
            {
                _actionResult.ActionData = _unitOfWork.TopicRepository.GetById(id);
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

        public async Task<ActionResult<Topic, int>> GetByIdAsync(int id)
        {
            try
            {
                _actionResult.ActionData = await _unitOfWork.TopicRepository.GetByIdAsync(id);
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

        public ActionResult<Topic, int> Create(Topic entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.Validate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = _unitOfWork.TopicRepository.Create(entity);
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

        public async Task<ActionResult<Topic, int>> CreateAsync(Topic entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.Validate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = await _unitOfWork.TopicRepository.CreateAsync(entity);
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

        public ActionResult<Topic, int> Update(Topic entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.ValidateUpdate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = _unitOfWork.TopicRepository.Update(entity);

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

        public async Task<ActionResult<Topic, int>> UpdateAsync(Topic entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.ValidateUpdate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = await _unitOfWork.TopicRepository.UpdateAsync(entity);

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

        public ActionResult<Topic, int> Delete(int id)
        {
            try
            {
                _unitOfWork.TopicRepository.Delete(id);
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

        public async Task<ActionResult<Topic, int>> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.TopicRepository.DeleteAsync(id);
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
