// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageService.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The access point for all interaction with the message-repository.
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
    /// The access point for all interaction with the message-repository.
    /// </summary>
    public class MessageService : IMessageService
    {
        /// <summary>
        /// The <see cref="ActionResult{TDomain,TId}"/> that will be returned by the service.
        /// </summary>
        private readonly ActionResult<Message, int> _actionResult = new ActionResult<Message, int>();

        /// <summary>
        /// The Unit Of Work, providing access to all repositories.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work ( gets injected through Ninject ).
        /// </param>
        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all messages persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public ActionResult<Message, int> GetAll()
        {
            try
            {
                _actionResult.ActionDataSet = _unitOfWork.MessageRepository.GetAll();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves all messages persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public async Task<ActionResult<Message, int>> GetAllAsync()
        {
            try
            {
                _actionResult.ActionDataSet = await _unitOfWork.MessageRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Retrieves a single message uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The messages unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves a single message uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The messages unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied message and - if successful, attempts to persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The message to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied message for creation and - if successful, attempts to asynchronously persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The message to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied message for update and - if successful, attempts to update it in the repository.
        /// If no message was found, the request is passed on to <see cref="Create"/> instead.
        /// </summary>
        /// <param name="entity">
        /// The message to validate and update ( or create, if it does not exist ).
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied message for update and - if successful, attempts to asynchronously update it in the repository.
        /// If no message was found, the request is passed on to <see cref="CreateAsync"/> instead.
        /// </summary>
        /// <param name="entity">
        /// The message to validate and update ( or create, if it does not exist ).
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Deletes a single message uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The messages unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public ActionResult<Message, int> Delete(int id)
        {
            try
            {
                _unitOfWork.MessageRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously deletes a single message uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The messages unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public async Task<ActionResult<Message, int>> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.MessageRepository.DeleteAsync(id);
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