using System;
using System.Collections.Generic;
using System.Text;

namespace Features.Extension
{
    /// <summary>
    /// This class is used to make extension methods.
    /// Extension methods come handy when working with Linq.
    /// </summary>
    public static class MyLinq
    {
        /// <summary>
        /// Get Number of elements in the enumerable object. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static int Count<T>(this IEnumerable<T> sequence)
        {
            int count = 0;
            foreach(var item in sequence)
            {
                count++;
            }
            return count;
        }
    }
}
