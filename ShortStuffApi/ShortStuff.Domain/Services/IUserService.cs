// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The UserService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Services
{
    using System.Threading.Tasks;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Helpers;

    /// <summary>
    /// The UserService interface.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all users persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<User, decimal> GetAll();

        /// <summary>
        /// Asynchronously retrieves all users persisted in the repository.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<User, decimal>> GetAllAsync();

        /// <summary>
        /// Retrieves a single user uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<User, decimal> GetById(decimal id);

        /// <summary>
        /// Asynchronously retrieves a single user uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<User, decimal>> GetByIdAsync(decimal id);

        /// <summary>
        /// Validates the supplied user and - if successful, attempts to persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The user to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<User, decimal> Create(User entity);

        /// <summary>
        /// Validates the supplied user for creation and - if successful, attempts to asynchronously persist it in the repository.
        /// </summary>
        /// <param name="entity">
        /// The user to validate and persist.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<User, decimal>> CreateAsync(User entity);

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
        ActionResult<User, decimal> Update(User entity);

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
        Task<ActionResult<User, decimal>> UpdateAsync(User entity);

        /// <summary>
        /// Deletes a single user uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<User, decimal> Delete(decimal id);

        /// <summary>
        /// Asynchronously deletes a single user uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The users unique identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<User, decimal>> DeleteAsync(decimal id);

        /// <summary>
        /// Retrieves a single user uniquely identified through their tag.
        /// </summary>
        /// <param name="tag">
        /// The unique tag by which to identify the user.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        ActionResult<User, decimal> GetByTag(string tag);

        /// <summary>
        /// Asynchronously retrieves a single user uniquely identified through their tag.
        /// </summary>
        /// <param name="tag">
        /// The unique tag by which to identify the user.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult{TDomain,TId}"/> exlpaining the result of the request, containing both data and debug information as appropriate.
        /// </returns>
        Task<ActionResult<User, decimal>> GetByTagAsync(string tag);
    }
}