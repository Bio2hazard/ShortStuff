using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using ShortStuff.Repository.ValueInjecter;

namespace ShortStuff.Repository.Extensions
{
    public static class LinqExtensions
    {
        private static readonly Dictionary<string, int> Dictionary = new Dictionary<string, int>();

        /// <summary>
        /// Adds the navigational property identified by value to be included in the query and entity mapping, recursing a maximum of depth times.
        /// </summary>
        /// <param name="value">Navigational Property to add</param>
        /// <param name="depth">Desired recursion depth</param>
        public static TSource With<TSource>(this TSource source, string value, int depth = 0)
        {
            Dictionary.Add(value, depth);
            return source;
        }

        /// <summary>
        /// Clears the navigational property dictionary
        /// </summary>
        public static void Reset()
        {
            Dictionary.Clear();
        }

        public static IEnumerable<TResult> BuildQuery<TSource, TResult>(this IQueryable<TSource> dbEntity) where TResult : class, new()
        {
            var query = dbEntity;

            foreach (var i in Dictionary)
            {
                query = query.Include(i.Key);
            }
            List<TSource> result = (from entity in query select entity).ToList();

            Reset();

            return Equals(result, default(TSource)) ? null : result.Select(u => u.BuildEntity(new TResult()));
        }

        public static TResult BuildQuery<TSource, TResult>(this IQueryable<TSource> dbEntity, Expression<Func<TSource, bool>> expression) where TResult : class, new()
        {
            var query = dbEntity;

            foreach (var i in Dictionary)
            {
                query = query.Include(i.Key);
            }
            TSource result = (from user in query select user).FirstOrDefault(expression);

            Reset();

            return Equals(result, default(TSource)) ? null : result.BuildEntity(new TResult());
        }

        /// <summary>
        /// Builds and executes a query, dynamically including desired navigational properties in a asynchronous fashion.
        /// The result is then mapped to the provided TResult business entity and returned as a list. 
        /// </summary>
        /// <returns>Null or a list of mapped domain Entities</returns>
        public static async Task<IEnumerable<TResult>> BuildQueryAsync<TSource, TResult>(this IQueryable<TSource> dbEntity) where TResult : class, new()
        {
            var query = dbEntity;
            var localDictionary = new Dictionary<string, int>(Dictionary);
            Reset();

            foreach (var i in localDictionary)
            {
                query = query.Include(i.Key);
            }
            List<TSource> result = await (from entity in query select entity).ToListAsync();

            return Equals(result, default(TSource)) ? null : result.Select(u => u.BuildEntity(new TResult(), localDictionary));
        }

        public static async Task<TResult> BuildQueryAsync<TSource, TResult>(this IQueryable<TSource> dbEntity, Expression<Func<TSource, bool>> expression) where TResult : class, new()
        {
            var query = dbEntity;
            var localDictionary = new Dictionary<string, int>(Dictionary);
            Reset();

            foreach (var i in localDictionary)
            {
                query = query.Include(i.Key);
            }
            TSource result = await (from user in query select user).FirstOrDefaultAsync(expression);

            return Equals(result, default(TSource)) ? null : result.BuildEntity(new TResult(), localDictionary);
        }

        public static TTarget BuildEntity<TSource, TTarget>(this TSource dbEntity, TTarget target)
        {
            return (TTarget)target.InjectFrom(new SinglePropertyDepthInjection(Dictionary), dbEntity);
        }

        /// <summary>
        /// Maps values from sourceEntity to targetEntity, recursing into properties defined in localDictionary.
        /// </summary>
        public static TTarget BuildEntity<TSource, TTarget>(this TSource sourceEntity, TTarget targetEntity, Dictionary<string, int> localDictionary)
        {
            return (TTarget)targetEntity.InjectFrom(new SinglePropertyDepthInjection(localDictionary), sourceEntity);
        }

        public static IEnumerable<TResult> InjectFromHelper<TSource, TResult>(this IEnumerable<TSource> source, TResult target, int depth)
        {
            return source.Select(x => target.InjectFrom(new MaxDepthCloneInjector(depth), x))
                            .Cast<TResult>().ToList();
        }
    }
}
