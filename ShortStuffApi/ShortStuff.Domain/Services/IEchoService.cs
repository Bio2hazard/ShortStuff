// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEchoService.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The EchoService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Services
{
    using System.Threading.Tasks;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Helpers;

    /// <summary>
    /// The EchoService interface.
    /// </summary>
    public interface IEchoService
    {
        /// <summary>
        /// Retrieves all echos persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Echo, int> GetAll();

        /// <summary>
        /// Asynchronously retrieves all echos persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Echo, int>> GetAllAsync();

        /// <summary>
        /// Retrieves a single echo uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Echo, int> GetById(int id);

        /// <summary>
        /// Asynchronously retrieves a single echo uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Echo, int>> GetByIdAsync(int id);

        /// <summary>
        /// Validates the supplied echo and - if successful, attempts to persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The echo to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Echo, int> Create(Echo entity);

        /// <summary>
        /// Validates the supplied echo for creation and - if successful, attempts to asynchronously persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The echo to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Echo, int>> CreateAsync(Echo entity);

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
        ActionResult<Echo, int> Update(Echo entity);

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
        Task<ActionResult<Echo, int>> UpdateAsync(Echo entity);

        /// <summary>
        /// Deletes a single echo uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Echo, int> Delete(int id);

        /// <summary>
        /// Asynchronously deletes a single echo uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The echos unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Echo, int>> DeleteAsync(int id);
    }
}