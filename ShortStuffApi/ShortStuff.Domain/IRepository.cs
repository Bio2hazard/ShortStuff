// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The Repository interface.
// </summary>
// <remark>
//   The domain layer makes it's requirements to the Repository Layer known through this file.
// </remark>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Helpers;

    /// <summary>
    /// The Repository interface.
    /// </summary>
    /// <typeparam name="TDomain">
    /// Type of the domain model.
    /// </typeparam>
    /// <typeparam name="TId">
    /// Type of the domain model's unique identifier. 
    /// </typeparam>
    public interface IRepository<TDomain, TId>
        where TDomain : EntityBase<TId>
    {
        /// <summary>
        /// Searches the repository and returns all elements matching Expression Type, Value and Predicate lamda.
        /// </summary>
        /// <param name="predicate">
        /// The predicate lambda used to specify the property.
        /// </param>
        /// <param name="value">
        /// The value the property is being compared to.
        /// </param>
        /// <param name="type">
        /// The ExpressionType used for the comparison.
        /// </param>
        /// <typeparam name="TSearch">
        /// The type of the property used by the predicate.
        /// </typeparam>
        /// <returns>
        /// IEnumerable TDomain result of the search.
        /// </returns>
        IEnumerable<TDomain> GetWhere<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type);

        /// <summary>
        /// Searches the repository asynchronously and returns all elements matching Expression Type, Value and Predicate lamda.
        /// </summary>
        /// <param name="predicate">
        /// The predicate lambda used to specify the property.
        /// </param>
        /// <param name="value">
        /// The value the property is being compared to.
        /// </param>
        /// <param name="type">
        /// The ExpressionType used for the comparison.
        /// </param>
        /// <typeparam name="TSearch">
        /// The type of the property used by the predicate.
        /// </typeparam>
        /// <returns>
        /// IEnumerable TDomain result of the search.
        /// </returns>
        Task<IEnumerable<TDomain>> GetWhereAsync<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type);

        /// <summary>
        /// Searches the repository and returns one elements matching Expression Type, Value and Predicate lamda, using FirstOrDefault.
        /// </summary>
        /// <param name="predicate">
        /// The predicate lambda used to specify the property.
        /// </param>
        /// <param name="value">
        /// The value the property is being compared to.
        /// </param>
        /// <param name="type">
        /// The ExpressionType used for the comparison.
        /// </param>
        /// <typeparam name="TSearch">
        /// The type of the property used by the predicate.
        /// </typeparam>
        /// <returns>
        /// The TDomain found by the search, or null.
        /// </returns>
        TDomain GetOne<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type);

        /// <summary>
        /// Searches the repository asynchronously and returns one elements matching Expression Type, Value and Predicate lamda, using FirstOrDefault.
        /// </summary>
        /// <param name="predicate">
        /// The predicate lambda used to specify the property.
        /// </param>
        /// <param name="value">
        /// The value the property is being compared to.
        /// </param>
        /// <param name="type">
        /// The ExpressionType used for the comparison.
        /// </param>
        /// <typeparam name="TSearch">
        /// The type of the property used by the predicate.
        /// </typeparam>
        /// <returns>
        /// The TDomain found by the search, or null.
        /// </returns>
        Task<TDomain> GetOneAsync<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type);

        /// <summary>
        /// Returns all elements in the respository.
        /// </summary>
        /// <returns>
        /// IEnumerable of all TDomain persisted in the database.
        /// </returns>
        IEnumerable<TDomain> GetAll();

        /// <summary>
        /// Asynchronously returns all elements in the respository.
        /// </summary>
        /// <returns>
        /// IEnumerable of all TDomain persisted in the database.
        /// </returns>
        Task<IEnumerable<TDomain>> GetAllAsync();

        /// <summary>
        /// Returns a single element from the repository, uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The unique id to find the element to retrieve.
        /// </param>
        /// <returns>
        /// The TDomain identified through id, or null.
        /// </returns>
        TDomain GetById(TId id);

        /// <summary>
        /// Asynchronously returns a single element from the repository, uniquely identified through id.
        /// </summary>
        /// <param name="id">
        /// The unique id to find the element to retrieve.
        /// </param>
        /// <returns>
        /// The TDomain identified through id, or null.
        /// </returns>
        Task<TDomain> GetByIdAsync(TId id);

        /// <summary>
        /// Persists a new TDomain element in the repository.
        /// </summary>
        /// <param name="entity">
        /// The element to persist.
        /// </param>
        /// <returns>
        /// The ActionStatus describing the success or failure of the action.
        /// </returns>
        ActionStatus<TId> Create(TDomain entity);

        /// <summary>
        /// Asynchronously persists a new TDomain element in the repository.
        /// </summary>
        /// <param name="entity">
        /// The element to persist.
        /// </param>
        /// <returns>
        /// The ActionStatus describing the success or failure of the action.
        /// </returns>
        Task<ActionStatus<TId>> CreateAsync(TDomain entity);

        /// <summary>
        /// Updates a existing element in the repository.
        /// </summary>
        /// <param name="entity">
        /// The element containing updated values.
        /// </param>
        /// <returns>
        /// The ActionStatus describing the success or failure of the action.
        /// </returns>
        ActionStatus<TId> Update(TDomain entity);

        /// <summary>
        /// Asynchronously updates a existing element in the repository.
        /// </summary>
        /// <param name="entity">
        /// The element containing updated values.
        /// </param>
        /// <returns>
        /// The ActionStatus describing the success or failure of the action.
        /// </returns>
        Task<ActionStatus<TId>> UpdateAsync(TDomain entity);

        /// <summary>
        /// Deletes the element uniquely identified by id from the repository.
        /// </summary>
        /// <param name="id">
        /// The unique id of the element to be deleted.
        /// </param>
        void Delete(TId id);

        /// <summary>
        /// Asynchronously deletes the element uniquely identified by id from the repository.
        /// </summary>
        /// <param name="id">
        /// The unique id of the element to be deleted.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/> enabling asynchronous processing.
        /// </returns>
        Task DeleteAsync(TId id);
    }
}