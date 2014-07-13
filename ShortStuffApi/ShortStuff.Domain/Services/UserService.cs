// ShortStuff.Domain
// UserService.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly ActionResult<User, decimal> _actionResult = new ActionResult<User, decimal>();
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult<User, decimal> GetAll()
        {
            try
            {
                _actionResult.ActionDataSet = _unitOfWork.UserRepository.GetAll();
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

        public async Task<ActionResult<User, decimal>> GetAllAsync()
        {
            try
            {
                _actionResult.ActionDataSet = await _unitOfWork.UserRepository.GetAllAsync();
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

        public ActionResult<User, decimal> GetById(decimal id)
        {
            try
            {
                _actionResult.ActionData = _unitOfWork.UserRepository.GetById(id);
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

        public async Task<ActionResult<User, decimal>> GetByIdAsync(decimal id)
        {
            try
            {
                _actionResult.ActionData = await _unitOfWork.UserRepository.GetByIdAsync(id);
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

        public ActionResult<User, decimal> Create(User entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.Validate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = _unitOfWork.UserRepository.Create(entity);
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

        public async Task<ActionResult<User, decimal>> CreateAsync(User entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.Validate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = await _unitOfWork.UserRepository.CreateAsync(entity);
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

        public ActionResult<User, decimal> Update(User entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.ValidateUpdate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = _unitOfWork.UserRepository.Update(entity);

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

        public async Task<ActionResult<User, decimal>> UpdateAsync(User entity)
        {
            try
            {
                _actionResult.ActionData = entity;
                if (!_actionResult.ValidateUpdate())
                {
                    return _actionResult;
                }

                _actionResult.ActionStatus = await _unitOfWork.UserRepository.UpdateAsync(entity);

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

        public ActionResult<User, decimal> Delete(decimal id)
        {
            try
            {
                _unitOfWork.UserRepository.Delete(id);
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

        public async Task<ActionResult<User, decimal>> DeleteAsync(decimal id)
        {
            try
            {
                await _unitOfWork.UserRepository.DeleteAsync(id);
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

        public ActionResult<User, decimal> GetByTag(string tag)
        {
            try
            {
                _actionResult.ActionData = _unitOfWork.UserRepository.GetOne(u => u.Tag, tag, ExpressionType.Equal);
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

        public async Task<ActionResult<User, decimal>> GetByTagAsync(string tag)
        {
            try
            {
                _actionResult.ActionData = await _unitOfWork.UserRepository.GetOneAsync(u => u.Tag, tag, ExpressionType.Equal);
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
    }
}
