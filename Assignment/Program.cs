using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

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

            //Task 6.
            JoinOperation();

            //Task 7
            ReadXML();

        }

        /// <summary>
        /// This method is used to perform task 6 of assignments that are:
        /// 1. Write a linq query to display the ProductId, ProductTitle and its Category Title.
        /// 2. Write a linq query to display the ProductTitle and Category Title in the format:
        ///     {ProductTitle: CategoryTitle}
        ///     and if there is no product associated with a particular category
        ///     it should show “No Product” instead of product title.
        /// </summary>
        private static void JoinOperation()
        {
            Console.WriteLine("\n\t\t\tTASK 6\n");
            var products = CreateProducts();
            var categories = CreateCategory();

            /// In this query join operation is performed on products and categories lists
            /// Join is performed on the basis of common column, here, CategoryId.
            var query = from product in products
                        join category in categories
                        on product.CategoryId equals category.CategoryId
                        select new
                        {
                            product.ProductId,
                            product.ProductTitle,
                            category.CategoryTitle
                        };

            foreach (var product in query)
            {
                Console.WriteLine($"{product.ProductId} : {product.ProductTitle,15} : {product.CategoryTitle}");
            }
            Console.WriteLine();

            /// In this query we are making product and category pairs, if there are no product 
            /// for an category, then "No Prodyct" will be displayed for that category.
            var query2 = from category in categories
                         join product in products
                         on category.CategoryId equals product.CategoryId
                         into gj  
                         from sub in gj.DefaultIfEmpty() 
                         select new
                         {
                             ProductTitle = sub?.ProductTitle ?? "No Product",
                             category.CategoryTitle
                         };

            foreach (var product in query2)
            {
                Console.WriteLine($"{product.ProductTitle,15} : {product.CategoryTitle}");
            }
        }

        /// <summary>
        /// This method will convert product csv file to list of object of class product.
        /// </summary>
        /// <returns></returns>
        private static List<Product> CreateProducts()
        {
            string path = "product.csv";
            var query = File.ReadAllLines(path)
                            .Where(l => l.Length > 1)
                            .Select(l =>
                            {
                                var column = l.Split(",");
                                return new Product
                                {
                                    ProductId = Convert.ToInt32(column[0]),
                                    ProductTitle = column[1],
                                    CategoryId = Convert.ToInt32(column[2])
                                };
                            });

            return query.ToList();
        }

        /// <summary>
        /// This method will convert category csv file to list of object of class category.
        /// </summary>
        /// <returns></returns>
        private static List<Category> CreateCategory()
        {
            string path = "category.csv";
            var query = File.ReadAllLines(path)
                            .Where(l => l.Length > 1)
                            .Select(l =>
                            {
                                var column = l.Split(",");
                                return new Category
                                {
                                    CategoryId = Convert.ToInt32(column[0]),
                                    CategoryTitle = column[1]
                                };
                            });

            return query.ToList();
        }

        /// <summary>
        /// This method performs all the queries in task 7.
        /// Here taks performed are:
        /// 1. Read XML via Linq.
        /// 2. Display Id, Title, Genre and Price of the Books, sorted by Title.
        /// 3. Display the Genre and count of the books under that genre.
        /// </summary>
        private static void ReadXML()
        {
            //  Load is used to load the xml file into an XDocument named "records".
            var records = XDocument.Load("books.xml");

            Console.WriteLine("\nDisplay Id, Title, Genre and Price of the Books, sorted by Title.\n");

            /// This query is selecting id, title, genre, price from the records
            /// In XML data is accessed with the help of "Element" and "Attribute" 
            /// property as data can either be an element or attribute.
            /// Here orderby is used to sort the data on the basis of title.
            var query = from book in records.Element("catalog").Elements("book")
                        orderby book.Element("title").Value
                        select new
                        {
                            Id = book.Attribute("id").Value,
                            Title = book.Element("title").Value,
                            Genre = book.Element("genre").Value,
                            Price = book.Element("price").Value
                        };

            foreach (var book in query)
            {
                Console.WriteLine($"{book.Id,6} : {book.Title,40} : {book.Genre,15} : {book.Price}");
            }

            Console.WriteLine("\nDisplay the Genre and count of the books under that genre.\n");

            /// To count the elements by grouping them according to genre.
            /// group by is used and grouping is done on the basis of genre
            /// then with projection movie count is calculated in each genre.
            var genreCounts = from book in records.Element("catalog").Elements("book")
                              group book by book.Element("genre").Value
                              into gGroup
                              select new
                              {
                                  Genre = gGroup.Key,
                                  Count = gGroup.Count()
                              };

            foreach (var book in genreCounts)
            {
                Console.WriteLine($"{book.Genre,15} : {book.Count}");
            }
        }

        /// <summary>
        /// This method is used to display a range of element from an array.
        /// </summary>
        /// <param name="start">It is the starting index.</param>
        /// <param name="end">It is the ending index.</param>
        private static void ExtractListFromPositions(int start, int end)
        {
            Console.WriteLine();
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
            Console.WriteLine();
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
