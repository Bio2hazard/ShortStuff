// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The access point for all interaction with the user-repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// todo: All these services employ mostly the same methods. It might be worthwhile to make a generic base service, to avoid repedetive code.
namespace ShortStuff.Domain.Services
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Enums;
    using ShortStuff.Domain.Helpers;

    /// <summary>
    /// The access point for all interaction with the user-repository.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// The <see cref="ActionResult{TDomain,TId}"/> that will be returned by the service.
        /// </summary>
        private readonly ActionResult<User, decimal> _actionResult = new ActionResult<User, decimal>();

        /// <summary>
        /// The Unit Of Work, providing access to all repositories.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work ( gets injected through Ninject ).
        /// </param>
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all users persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}" /> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public ActionResult<User, decimal> GetAll()
        {
            try
            {
                _actionResult.ActionDataSet = _unitOfWork.UserRepository.GetAll();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves all users persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public async Task<ActionResult<User, decimal>> GetAllAsync()
        {
            try
            {
                _actionResult.ActionDataSet = await _unitOfWork.UserRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Retrieves a single user uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves a single user uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied user and - if successful, attempts to persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The user to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied user for creation and - if successful, attempts to asynchronously persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The user to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied user for update and - if successful, attempts to update it in the repository.
        /// If no user was found, the request is passed on to <see cref="Create"/> instead.
        /// </summary>
        /// <param name="entity">
        /// The user to validate and update ( or create, if it does not exist ).
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied user for update and - if successful, attempts to asynchronously update it in the repository.
        /// If no user was found, the request is passed on to <see cref="CreateAsync"/> instead.
        /// </summary>
        /// <param name="entity">
        /// The user to validate and update ( or create, if it does not exist ).
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Deletes a single user uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public ActionResult<User, decimal> Delete(decimal id)
        {
            try
            {
                _unitOfWork.UserRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously deletes a single user uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public async Task<ActionResult<User, decimal>> DeleteAsync(decimal id)
        {
            try
            {
                await _unitOfWork.UserRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Retrieves a single user uniquely identified through their tag.
        /// </summary>
        /// <param name="tag">
        /// The unique tag by which to identify the user.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves a single user uniquely identified through their tag.
        /// </summary>
        /// <param name="tag">
        /// The unique tag by which to identify the user.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }
    }
}