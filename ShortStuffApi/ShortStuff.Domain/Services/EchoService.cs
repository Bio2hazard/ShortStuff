// ShortStuff.Domain
// EchoService.cs
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
    public class EchoService : IEchoService
    {
        private readonly ActionResult<Echo, int> _actionResult = new ActionResult<Echo, int>();
        private readonly IUnitOfWork _unitOfWork;

        public EchoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult<Echo, int> GetAll()
        {
            try
            {
                _actionResult.ActionDataSet = _unitOfWork.EchoRepository.GetAll();
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

        public async Task<ActionResult<Echo, int>> GetAllAsync()
        {
            try
            {
                _actionResult.ActionDataSet = await _unitOfWork.EchoRepository.GetAllAsync();
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

        public ActionResult<Echo, int> GetById(int id)
        {
            try
            {
                _actionResult.ActionData = _unitOfWork.EchoRepository.GetById(id);
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

        public async Task<ActionResult<Echo, int>> GetByIdAsync(int id)
        {
            try
            {
                _actionResult.ActionData = await _unitOfWork.EchoRepository.GetByIdAsync(id);
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

        public ActionResult<Echo, int> Create(Echo entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.Validate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = _unitOfWork.EchoRepository.Create(entity);
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

        public async Task<ActionResult<Echo, int>> CreateAsync(Echo entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.Validate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = await _unitOfWork.EchoRepository.CreateAsync(entity);
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

        public ActionResult<Echo, int> Update(Echo entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.ValidateUpdate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = _unitOfWork.EchoRepository.Update(entity);

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

        public async Task<ActionResult<Echo, int>> UpdateAsync(Echo entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.ValidateUpdate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = await _unitOfWork.EchoRepository.UpdateAsync(entity);

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

        public ActionResult<Echo, int> Delete(int id)
        {
            try
            {
                _unitOfWork.EchoRepository.Delete(id);
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

        public async Task<ActionResult<Echo, int>> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.EchoRepository.DeleteAsync(id);
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
