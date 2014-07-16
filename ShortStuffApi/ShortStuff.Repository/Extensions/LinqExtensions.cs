// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinqExtensions.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The linq extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Omu.ValueInjecter;

    using ShortStuff.Repository.ValueInjecter;

    /// <summary>
    /// The linq extensions.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// The dictionary.
        /// </summary>
        private static readonly Dictionary<string, int> Dictionary = new Dictionary<string, int>();

        /// <summary>
        /// Adds the navigational property identified by value to be included in the query and entity mapping, recursing a
        ///     maximum of depth times.
        /// </summary>
        /// <param name="source">
        /// Generic Type Source
        /// </param>
        /// <param name="value">
        /// Navigational Property to add
        /// </param>
        /// <param name="depth">
        /// Desired recursion depth
        /// </param>
        /// <returns>
        /// The <see cref="TSource"/>.
        /// </returns>
        public static TSource With<TSource>(this TSource source, string value, int depth = 0)
        {
            Dictionary.Add(value, depth);
            return source;
        }

        /// <summary>
        ///     Clears the navigational property dictionary
        /// </summary>
        public static void Reset()
        {
            Dictionary.Clear();
        }

        /// <summary>
        /// The build query.
        /// </summary>
        /// <param name="dbEntity">
        /// The db entity.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<TResult> BuildQuery<TSource, TResult>(this IQueryable<TSource> dbEntity) where TResult : class, new()
        {
            var query = dbEntity;

            foreach (var i in Dictionary)
            {
                query = query.Include(i.Key);
            }

            var result = (from entity in query select entity).ToList();

            Reset();

            return Equals(result, default(TSource)) ? null : result.Select(u => u.BuildEntity(new TResult()));
        }

        /// <summary>
        /// The build query.
        /// </summary>
        /// <param name="dbEntity">
        /// The db entity.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TResult"/>.
        /// </returns>
        public static TResult BuildQuery<TSource, TResult>(this IQueryable<TSource> dbEntity, Expression<Func<TSource, bool>> expression) where TResult : class, new()
        {
            var query = dbEntity;

            foreach (var i in Dictionary)
            {
                query = query.Include(i.Key);
            }

            var result = (from user in query select user).FirstOrDefault(expression);

            Reset();

            return Equals(result, default(TSource)) ? null : result.BuildEntity(new TResult());
        }

        /// <summary>
        /// Builds and executes a query, dynamically including desired navigational properties in a asynchronous fashion.
        ///     The result is then mapped to the provided TResult business entity and returned as a list.
        /// </summary>
        /// <param name="dbEntity">
        /// The db Entity.
        /// </param>
        /// <returns>
        /// Null or a list of mapped domain Entities
        /// </returns>
        public static async Task<IEnumerable<TResult>> BuildQueryAsync<TSource, TResult>(this IQueryable<TSource> dbEntity) where TResult : class, new()
        {
            var query = dbEntity;
            var localDictionary = new Dictionary<string, int>(Dictionary);
            Reset();

            foreach (var i in localDictionary)
            {
                query = query.Include(i.Key);
            }

            var result = await (from entity in query select entity).ToListAsync();

            return Equals(result, default(TSource)) ? null : result.Select(u => u.BuildEntity(new TResult(), localDictionary));
        }

        /// <summary>
        /// The build query async.
        /// </summary>
        /// <param name="dbEntity">
        /// The db entity.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<TResult> BuildQueryAsync<TSource, TResult>(this IQueryable<TSource> dbEntity, Expression<Func<TSource, bool>> expression)
            where TResult : class, new()
        {
            var query = dbEntity;
            var localDictionary = new Dictionary<string, int>(Dictionary);
            Reset();

            foreach (var i in localDictionary)
            {
                query = query.Include(i.Key);
            }

            var result = await (from user in query select user).FirstOrDefaultAsync(expression);

            return Equals(result, default(TSource)) ? null : result.BuildEntity(new TResult(), localDictionary);
        }

        /// <summary>
        /// The build entity.
        /// </summary>
        /// <param name="sourceEntity">
        /// The source entity.
        /// </param>
        /// <param name="targetEntity">
        /// The target entity.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <typeparam name="TTarget">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TTarget"/>.
        /// </returns>
        public static TTarget BuildEntity<TSource, TTarget>(this TSource sourceEntity, TTarget targetEntity) where TTarget : class
        {
            return Equals(sourceEntity, default(TSource)) ? null : (TTarget)targetEntity.InjectFrom(new SinglePropertyDepthInjection(Dictionary), sourceEntity);
        }

        /// <summary>
        /// Maps values from sourceEntity to targetEntity, recursing into properties defined in localDictionary.
        /// </summary>
        /// <param name="sourceEntity">
        /// The source Entity.
        /// </param>
        /// <param name="targetEntity">
        /// The target Entity.
        /// </param>
        /// <param name="localDictionary">
        /// The local Dictionary.
        /// </param>
        /// <returns>
        /// The <see cref="TTarget"/>.
        /// </returns>
        public static TTarget BuildEntity<TSource, TTarget>(this TSource sourceEntity, TTarget targetEntity, Dictionary<string, int> localDictionary) where TTarget : class
        {
            return Equals(sourceEntity, default(TSource)) ? null : (TTarget)targetEntity.InjectFrom(new SinglePropertyDepthInjection(localDictionary), sourceEntity);
        }

        /// <summary>
        /// The inject from helper.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="depth">
        /// The depth.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<TResult> InjectFromHelper<TSource, TResult>(this IEnumerable<TSource> source, TResult target, int depth)
        {
            return source.Select(x => target.InjectFrom(new MaxDepthCloneInjector(depth), x)).Cast<TResult>().ToList();
        }

        /// <summary>
        /// The lambda.
        /// </summary>
        /// <param name="dbEntity">
        /// The db entity.
        /// </param>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <typeparam name="TId">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<TSource> Lambda<TSource, TId>(
            this IQueryable<TSource> dbEntity, 
            Expression<Func<TSource, TId>> property, 
            TId value, 
            ExpressionType type = ExpressionType.Equal)
        {
            var left = property.Body;
            Expression right = Expression.Constant(value, typeof(TId));
            Expression condition = Expression.MakeBinary(type, left, right);
            var lambda = Expression.Lambda<Func<TSource, bool>>(condition, new[] { property.Parameters.Single() });
            return dbEntity.Where(lambda);
        }

        /// <summary>
        /// The convert expression.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <typeparam name="TTarget">
        /// </typeparam>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <typeparam name="TSearch">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression<Func<TTarget, TSearch>> ConvertExpression<TTarget, TSource, TSearch>(this Expression<Func<TSource, TSearch>> predicate)
        {
            var leftParameter = Expression.Parameter(typeof(TTarget));
            var parameterType = predicate.Parameters[0].Type;

            var memberExpression = predicate.Body as MemberExpression;
            if (memberExpression == null)
            {
                return null;
            }

            var parameterName = parameterType.GetMember(memberExpression.Member.Name)[0];

            var condition = Expression.Property(leftParameter, typeof(TTarget), parameterName.Name);
            return Expression.Lambda<Func<TTarget, TSearch>>(condition, leftParameter);
        }
    }
}