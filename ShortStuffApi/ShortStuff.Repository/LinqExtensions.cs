using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using ShortStuff.Repository.ValueInjecter;

namespace ShortStuff.Repository
{
    public static class LinqExtensions
    {
        public static IEnumerable<TResult> InjectFromHelper<TSource, TResult>(this IEnumerable<TSource> source, TResult target, int depth)
        {
            return source.Select(x => target.InjectFrom(new MaxDepthCloneInjector(depth), x))
                            .Cast<TResult>().ToList();
        }
    }
}
