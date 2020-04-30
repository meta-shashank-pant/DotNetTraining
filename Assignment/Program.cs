using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task 1.
            #region Difference Between Linq and Stored Procedure.
            /*  LINQ provides you common query syntax to query various data sources 
                like SQL Server, Oracle, DB2, web services, XML and Collection, etc.
                On the other hand, a stored procedure is a pre-compiled set of one or more 
                SQL statements that are stored on RDBMS (SQL Server, Oracle, DB2 and MySQL, etc.).
                Some Differences are:
                1. Stored procedures are faster as compared to LINQ query since they have a 
                    predictable execution plan and can take the full advantage of SQL features.
                2. LINQ has full type checking at compile-time and Intellisense support in 
                    Visual Studio as compared to a stored procedure.
                3. LINQ allows debugging through .NET debugger as compared to a stored procedure.
                4. LINQ supports features like multithreading as compared to stored procedures.
                5. LINQ provides the uniform programming model to query the multiple databases 
                    while you need to re-write the stored procedure for different databases.
                6. Stored procedure is good for writing complex queries compared to LINQ.
                7. Deploying a LINQ based application is much easy and simple as compared 
                    to stored procedures based. Since in case of stored procedures, you need 
                    to provide a SQL script for deployment but in case of LINQ, everything 
                    gets compiled into the DLLs. Hence you need to deploy only DLLs.
             */
            #endregion

            //Task 2.
            #region Difference between First() and FirstOrDefault() operator.
            /*  The major difference between First and FirstOrDefault is that First() 
             *  will throw an exception if there is no result data for the supplied 
             *  criteria whereas FirstOrDefault() will return the default value (null) 
             *  if there is no result data.
             *  Example is shown in Practice/Cars/program.cs inside the First and Last region.
             */
            #endregion

            //Task 3.
            GetOddNumbers();

            //Task 4.
            string sentence = "Extract UPPER case words FROM THIS string";
            FilterAndDisplayUpperCaseWords(sentence);

            //Task 5.
            int start = 2, end = 5;
            ExtractListFromPositions(start, end);
            
        }

        /// <summary>
        /// This method is used to display a range of element from an array.
        /// </summary>
        /// <param name="start">It is the starting index.</param>
        /// <param name="end">It is the ending index.</param>
        private static void ExtractListFromPositions(int start, int end)
        {
            if (start > end || start < 1 || end > 7)
            {
                return;
            }
            string[] names = { "John", "Mike", "Chris", "Jack", "Andy", "Gretchen", "Helen"};

            /// In this query two operators are used:
            /// 1. Skip: It is used to skip the values from the top of the collection.
            /// 2. Take: It is used for taking the specific amount of value from collection.
            var selectedName = names.Skip(start - 1).Take(end - start + 1);

            foreach (var name in selectedName)
            {
                Console.WriteLine(name);
            }
        }

        /// <summary>
        /// This method is used to filter and display the upper case words in sentence.
        /// </summary>
        /// <param name="sentence"></param>
        private static void FilterAndDisplayUpperCaseWords(string sentence)
        {
            IEnumerable<string> words = sentence.Split(" ");

            //This query is helping in checking if the word is in uppercase or not.
            var upperCaseWords = words.Where(w => w == w.ToUpper());

            foreach (var word in upperCaseWords)
            {
                Console.WriteLine(word);
            }
        }

        /// <summary>
        /// This method is used to retrieve odd numbers from a list of natural numbers.
        /// </summary>
        private static void GetOddNumbers()
        {
            List<int> numbers = new List<int>();
            //Adding numbers to the list.
            for (int i = 0; i < 20; i++)
            {
                numbers.Add(i + 1);
            }
            /// Where query for getting the odd numbers.
            /// In this query, i used lambda expression for odd condition,
            /// it is the most efficient way of writing the query.
            var oddNumbers = numbers.Where(num => num % 2 == 1);
            foreach (var number in oddNumbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
