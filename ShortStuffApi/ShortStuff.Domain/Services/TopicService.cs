// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopicService.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The access point for all interaction with the topic-repository.
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
    /// The access point for all interaction with the topic-repository.
    /// </summary>
    public class TopicService : ITopicService
    {
        /// <summary>
        /// The <see cref="ActionResult{TDomain,TId}"/> that will be returned by the service.
        /// </summary>
        private readonly ActionResult<Topic, int> _actionResult = new ActionResult<Topic, int>();

        /// <summary>
        /// The Unit Of Work, providing access to all repositories.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work ( gets injected through Ninject ).
        /// </param>
        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all topics persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public ActionResult<Topic, int> GetAll()
        {
            try
            {
                _actionResult.ActionDataSet = _unitOfWork.TopicRepository.GetAll();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves all topics persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public async Task<ActionResult<Topic, int>> GetAllAsync()
        {
            try
            {
                _actionResult.ActionDataSet = await _unitOfWork.TopicRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Retrieves a single topic uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves a single topic uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied topic and - if successful, attempts to persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The topic to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied topic for creation and - if successful, attempts to asynchronously persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The topic to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied topic for update and - if successful, attempts to update it in the repository.
        /// If no topic was found, the request is passed on to <see cref="Create"/> instead.
        /// </summary>
        /// <param name="entity">
        /// The topic to validate and update ( or create, if it does not exist ).
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied topic for update and - if successful, attempts to asynchronously update it in the repository.
        /// If no topic was found, the request is passed on to <see cref="CreateAsync"/> instead.
        /// </summary>
        /// <param name="entity">
        /// The topic to validate and update ( or create, if it does not exist ).
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Deletes a single topic uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public ActionResult<Topic, int> Delete(int id)
        {
            try
            {
                _unitOfWork.TopicRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously deletes a single topic uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public async Task<ActionResult<Topic, int>> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.TopicRepository.DeleteAsync(id);
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