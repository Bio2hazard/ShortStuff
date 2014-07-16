// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EchoService.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The access point for all interaction with the echo-repository.
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
    /// The access point for all interaction with the echo-repository.
    /// </summary>
    public class EchoService : IEchoService
    {
        /// <summary>
        /// The <see cref="ActionResult{TDomain,TId}"/> that will be returned by the service.
        /// </summary>
        private readonly ActionResult<Echo, int> _actionResult = new ActionResult<Echo, int>();

        /// <summary>
        /// The Unit Of Work, providing access to all repositories.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EchoService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work ( gets injected through Ninject ).
        /// </param>
        public EchoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all echos persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public ActionResult<Echo, int> GetAll()
        {
            try
            {
                _actionResult.ActionDataSet = _unitOfWork.EchoRepository.GetAll();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves all echos persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public async Task<ActionResult<Echo, int>> GetAllAsync()
        {
            try
            {
                _actionResult.ActionDataSet = await _unitOfWork.EchoRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Retrieves a single echo uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously retrieves a single echo uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied echo and - if successful, attempts to persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The echo to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied echo for creation and - if successful, attempts to asynchronously persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The echo to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied echo for update and - if successful, attempts to update it in the repository.
        /// If no echo was found, the request is passed on to <see cref="Create"/> instead.
        /// </summary>
        /// <param name="entity">
        /// The echo to validate and update ( or create, if it does not exist ).
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Validates the supplied echo for update and - if successful, attempts to asynchronously update it in the repository.
        /// If no echo was found, the request is passed on to <see cref="CreateAsync"/> instead.
        /// </summary>
        /// <param name="entity">
        /// The echo to validate and update ( or create, if it does not exist ).
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
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
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Deletes a single echo uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public ActionResult<Echo, int> Delete(int id)
        {
            try
            {
                _unitOfWork.EchoRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _actionResult.ActionStatus.Status = ActionStatusEnum.ExceptionError;
                _actionResult.ActionException = ex;
            }

            return _actionResult;
        }

        /// <summary>
        /// Asynchronously deletes a single echo uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        public async Task<ActionResult<Echo, int>> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.EchoRepository.DeleteAsync(id);
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