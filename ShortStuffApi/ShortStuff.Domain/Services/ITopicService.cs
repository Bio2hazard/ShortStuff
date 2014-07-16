// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITopicService.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The TopicService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Services
{
    using System.Threading.Tasks;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Helpers;

    /// <summary>
    /// The TopicService interface.
    /// </summary>
    public interface ITopicService
    {
        /// <summary>
        /// Retrieves all topics persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Topic, int> GetAll();

        /// <summary>
        /// Asynchronously retrieves all topics persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Topic, int>> GetAllAsync();

        /// <summary>
        /// Retrieves a single topic uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Topic, int> GetById(int id);

        /// <summary>
        /// Asynchronously retrieves a single topic uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Topic, int>> GetByIdAsync(int id);

        /// <summary>
        /// Validates the supplied topic and - if successful, attempts to persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The topic to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Topic, int> Create(Topic entity);

        /// <summary>
        /// Validates the supplied topic for creation and - if successful, attempts to asynchronously persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The topic to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Topic, int>> CreateAsync(Topic entity);

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
        ActionResult<Topic, int> Update(Topic entity);

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
        Task<ActionResult<Topic, int>> UpdateAsync(Topic entity);

        /// <summary>
        /// Deletes a single topic uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Topic, int> Delete(int id);

        /// <summary>
        /// Asynchronously deletes a single topic uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The topics unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Topic, int>> DeleteAsync(int id);
    }
}