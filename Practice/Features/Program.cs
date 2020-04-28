using System;
using System.Collections.Generic;
using System.Linq;
//using Features.Extension;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            //Below is the normal way to do coding, i.e make array or list.
            //Both array and list implements IEnumerable, so we can instead use them also.

            /*
            Employee[] developers = new Employee[] 
            {
                new Employee{Id = 1, Name = "Steve"},
                new Employee{Id = 2, Name = "Clay"}
            };

            List<Employee> sales = new List<Employee>()
            {
                new Employee{Id = 3, Name = "Wayne"}
            };

            foreach(var person in developers)
            {
                Console.WriteLine(person.Name);
            }
            */

            /*Using IEnumerable instead.
             * IEnumerable is very important interface for LINQ, the query operations that we can perform using LINQ
             * are mostly done with the help of this interface.
             */
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee{Id = 1, Name = "Steve"},
                new Employee{Id = 2, Name = "Clay"},
                new Employee{Id = 4, Name = "Jacky"}
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee{Id = 3, Name = "Wayne"}
            };

            /*Console.WriteLine("Count = " + developers.Count());

            IEnumerator<Employee> enumerator = developers.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }*/

            //This method of writing query is known as query method syntax approach.
            foreach (var item in developers.Where(e => e.Name.Length == 5).OrderBy(e => e.Name))
            {
                Console.WriteLine("** " + item.Name);
            }

            //LambdaExpression(developers);
            Console.WriteLine();
            FuncExample();
            Console.WriteLine();
            ActionExample();
            Console.WriteLine();
            QuerySyntax(developers);
        }

        static void QuerySyntax(IEnumerable<Employee> developers)
        {
            //Method Syntax
            var query1 = developers.Where(e => e.Name.Length == 5).OrderBy(e => e.Name);
            //Query Syntax
            var query2 = from developer in developers
                         where developer.Name.Length == 5
                         orderby developer.Name descending
                         select developer;

            foreach (var item in query2)
            {
                Console.WriteLine(item.Name);
            }
        }

        #region Func and Action
        /// <summary>
        /// Func in Linq, Func van have 17 variation, last param in func points to the return type of method.
        /// Other than last every param is the argument for that function.
        /// </summary>
        static void FuncExample()
        {
            Func<int, int> f = Square;
            Console.WriteLine(f(3));
            //          OR
            Func<int, int> square = num => num * num;
            Console.WriteLine(square(4));

            //We can also add the method body with lambda expression.
            Func<int, int, int> add = (x, y) => x + y;
            //          OR
            Func<int, int, int> addBody = (x, y) =>
            {
                int temp = x + y;
                return temp;
            };
        }

        /// <summary>
        /// Action are similar to Func the only difference is that they do not return value.
        /// </summary>
        static void ActionExample()
        {
            Action<int> writeSomething = x => Console.WriteLine($"Action called value: {x}");
        }

        private static int Square(int num)
        {
            return num * num;
        }

        #endregion

        #region Lambda Expression

        static void LambdaExpression(IEnumerable<Employee> developers)
        {
            //Use with Named method.
            foreach (var item in developers.Where(NameStartWithS))
            {
                Console.WriteLine(item.Name);
            }

            //Use with Anonymous method.
            foreach (var item in developers.Where(
                delegate (Employee employee)
                {
                    return employee.Name.StartsWith('S');
                }))
            {
                Console.WriteLine(item.Name);
            }

            //Using lambda expression
            foreach (var item in developers.Where(employee => employee.Name.StartsWith('S')))
            {
                Console.WriteLine(item.Name);
            }

        }        

        //Named method for where clause.(Not good for long codes)
        private static bool NameStartWithS(Employee employee)
        {
            return employee.Name.StartsWith('S');
        }
        #endregion
    }
}
