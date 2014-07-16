// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The access point for all interaction with the notification-repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// todo: All these services employ mostly the same methods. It might be worthwhile to make a generic base service, to avoid repedetive code.
namespace ShortStuff.Domain.Services
{
    using System;
    using System.Threading.Tasks;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Enums;
    using ShortStuff.Domain.Helpers;

    /// <summary>
    /// The access point for all interaction with the notification-repository.
    /// </summary>
    public class NotificationService : INotificationService
    {
        /// <summary>
        /// The <see cref="ActionResult{TDomain,TId}"/> that will be returned by the service.
        /// </summary>
        private readonly ActionResult<Notification, int> _actionResult = new ActionResult<Notification, int>();

        /// <summary>
        /// The Unit Of Work, providing access to all repositories.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work ( gets injected through Ninject ).
        /// </param>
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all notifications persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public ActionResult<Notification, int> GetAll()
        {
            try
            {
                _actionResult.ActionDataSet = _unitOfWork.NotificationRepository.GetAll();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves all notifications persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public async Task<ActionResult<Notification, int>> GetAllAsync()
        {
            try
            {
                _actionResult.ActionDataSet = await _unitOfWork.NotificationRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Retrieves a single notification uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves a single notification uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied notification and - if successful, attempts to persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The notification to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied notification for creation and - if successful, attempts to asynchronously persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The notification to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied notification for update and - if successful, attempts to update it in the repository.
        /// If no notification was found, the request is passed on to <see cref="Create"/> instead.
        /// </summary>
        /// <param name="entity">
        /// The notification to validate and update ( or create, if it does not exist ).
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied notification for update and - if successful, attempts to asynchronously update it in the repository.
        /// If no notification was found, the request is passed on to <see cref="CreateAsync"/> instead.
        /// </summary>
        /// <param name="entity">
        /// The notification to validate and update ( or create, if it does not exist ).
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Deletes a single notification uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public ActionResult<Notification, int> Delete(int id)
        {
            try
            {
                _unitOfWork.NotificationRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously deletes a single notification uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public async Task<ActionResult<Notification, int>> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.NotificationRepository.DeleteAsync(id);
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