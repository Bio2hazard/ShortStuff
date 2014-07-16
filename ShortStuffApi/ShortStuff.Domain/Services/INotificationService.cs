// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotificationService.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The NotificationService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Services
{
    using System.Threading.Tasks;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Helpers;

    /// <summary>
    /// The NotificationService interface.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Retrieves all notifications persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Notification, int> GetAll();

        /// <summary>
        /// Asynchronously retrieves all notifications persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Notification, int>> GetAllAsync();

        /// <summary>
        /// Retrieves a single notification uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Notification, int> GetById(int id);

        /// <summary>
        /// Asynchronously retrieves a single notification uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Notification, int>> GetByIdAsync(int id);

        /// <summary>
        /// Validates the supplied notification and - if successful, attempts to persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The notification to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Notification, int> Create(Notification entity);

        /// <summary>
        /// Validates the supplied notification for creation and - if successful, attempts to asynchronously persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The notification to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Notification, int>> CreateAsync(Notification entity);

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
        ActionResult<Notification, int> Update(Notification entity);

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
        Task<ActionResult<Notification, int>> UpdateAsync(Notification entity);

        /// <summary>
        /// Deletes a single notification uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<Notification, int> Delete(int id);

        /// <summary>
        /// Asynchronously deletes a single notification uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The notifications unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<Notification, int>> DeleteAsync(int id);
    }
}