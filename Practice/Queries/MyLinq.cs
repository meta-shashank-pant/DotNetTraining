using System;
using System.Collections.Generic;
using System.Text;

namespace Queries
{
    public static class MyLinq
    {
        /// <summary>
        /// This is the extension method for any object class who implements IEnumerable.
        /// This is the one implementation of filter, but Linq actually not implement where like this.
        /// </summary>
        /// <typeparam name="T">This is the generic type</typeparam>
        /// <param name="source">It is an list of some specific datatype/ object.</param>
        /// <param name="predicate">Func is passed for checking if result is true or not for particular condition.</param>
        /// <returns>The resultant list is returned.</returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            //var list = new List<T>();

            foreach (var item in source)
            {
                /*
                 "yield" operator: Yield helps in building IEnumerable. Return type should be IEnumerable
                    or IEnumerator, a data structure is builded which has sequence of items that can 
                    be iterate over using the foreach loop.
                    Execution will start only when we will be pulling something out of Ienumerable
                    So execution will continue untill a yield return statement will found, the yield statement
                    throw control back to the caller returining an item.
                    So now the output will be somewhat similar to what linq produces.
                    Yield statement that give us behaviour known as deffered execution
                 */
                if (predicate(item))
                {
                    yield return item;
                }
            }
            //return list;
        }

        public static IEnumerable<Double> Random()
        {
            var random = new Random();
            while (true)
            {
                yield return random.NextDouble();
            }
        }
    }
}
