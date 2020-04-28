using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = MyLinq.Random().Where(m => m > 0.5).Take(10);
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            IEnumerable<Movie> movies = new List<Movie>
            {
                new Movie { Title = "The Dark Night", Rating = 8.9f, Year = 2008},
                new Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010},
                new Movie { Title = "Casablanca", Rating = 8.5f, Year = 1942},
                new Movie { Title = "Star Wars V", Rating = 8.7f, Year = 1980}
            };

            #region Defferd Execution

            //var queries = movies.Where(mov => mov.Year >= 2000);
            var queries = movies.Filter(m => m.Year >= 2000);

            Console.WriteLine($"count = {queries.Count()}");

            IEnumerator<Movie> iterator = queries.GetEnumerator();

            while (iterator.MoveNext())
            {
                Console.WriteLine(iterator.Current.Title);
            }

            #endregion

            #region Immediate Execution
            /*
             Disadvantage of deffered execution:
                Suppose if we had a collection of 10000 movies, deffered execution
                can help if we had an operator like Take(10) because that means we need
                not iterate through the entire collection of movies.
                But for same size of data if we are doing multiple operation like 
                printing, count, etc. So for that condition we should not use deffered
                execution as result we be evaluated multiple times taking memory and time
                so instead we can do something that will immediately execute that query
                and get the concrete result.
                Another place where deffered execution can trip us off, when we are trying
                to deal with exceptions and errors in a query.
                Linq provides such sort of operation and most of them start with "To"
                ex: ToArray, ToList, ToDictionary, etc.
             */

            var query = movies.Filter(m => m.Year >= 2000).ToList();

            Console.WriteLine($"length = {query.Count()}");

            //We can now also do some other operations.

            #endregion

            #region Streaming Operator and Non-Streaming Operator

            /*
             There can be 2 type of execution:
                1. Deffered
                2. Immediate

                A deffered execution can also categorized into 2 category.
                1. Streaming Operator:
                    "Where" is an example of streaming operator, it only needs to read the data up into 
                    the point where it produces a result. At that point it yield the result and jump out
                    of the where method and we can process that single item.

                2. Non-Streaming Operator
                    "OrderBy" or "OrderByDescending" are the example of Non-Streaming Operator, beacuse
                    once OrderBy starts to execute, it goes through the entire incoming sequence of items
             */
            var query1 = movies.Where(e => e.Year >= 2000).OrderByDescending(e => e.Rating);
            foreach (var item in query1)
            {
                Console.WriteLine("## "+item.Title);
            }

            #endregion
        }
    }
}
///Important shortcut
///ctrl + k + c for commenting multiple line.
///F10 to start debugger and F10(Step Over) and F11(Step Into) to step through process
