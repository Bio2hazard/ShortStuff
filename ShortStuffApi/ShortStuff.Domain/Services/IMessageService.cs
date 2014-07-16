// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageService.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The MessageService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Services
{
    using System.Threading.Tasks;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Helpers;

    /// <summary>
    /// The MessageService interface.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Retrieves all messages persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Message, int> GetAll();

        /// <summary>
        /// Asynchronously retrieves all messages persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Message, int>> GetAllAsync();

        /// <summary>
        /// Retrieves a single message uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The messages unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Message, int> GetById(int id);

        /// <summary>
        /// Asynchronously retrieves a single message uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The messages unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Message, int>> GetByIdAsync(int id);

        /// <summary>
        /// Validates the supplied message and - if successful, attempts to persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The message to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Message, int> Create(Message entity);

        /// <summary>
        /// Validates the supplied message for creation and - if successful, attempts to asynchronously persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The message to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Message, int>> CreateAsync(Message entity);

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
        ActionResult<Message, int> Update(Message entity);

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
        Task<ActionResult<Message, int>> UpdateAsync(Message entity);

        /// <summary>
        /// Deletes a single message uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The messages unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Message, int> Delete(int id);

        /// <summary>
        /// Asynchronously deletes a single message uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The messages unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Message, int>> DeleteAsync(int id);
    }
}