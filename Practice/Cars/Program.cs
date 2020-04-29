using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturer("manufacturers.csv");
            GetFuelEfficientCarByCountry(cars, manufacturers);
            
        }

        private static List<Manufacturer> ProcessManufacturer(string path)
        {
            var query = File.ReadAllLines(path)
                            .Where(l => l.Length > 1)
                            .Select(l =>
                            {
                                var column = l.Split(",");
                                return new Manufacturer
                                {
                                    Name = column[0],
                                    Headquarters = column[1],
                                    Year = Convert.ToInt32(column[2])
                                };
                            });

            return query.ToList();
        }

        /// <summary>
        /// This method will process the csv file and create an list if cars from that.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static List<Car> ProcessCars(string path)
        {
            return
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCar()
                    .ToList();
        }

        #region Joining Data

        static void JoinData(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            var query1 = from car in cars
                        join manufacturer in manufacturers
                            on car.Manufacturer equals manufacturer.Name
                        orderby car.Combined descending, car.Name ascending
                        select new
                        {
                            manufacturer.Headquarters,
                            car.Name,
                            car.Combined
                        };

            /// Keep in mind: Generally inner sequence is smaller than outer sequence in Join method.
            /// Like here manufacturer data is smaller than cars.
            /// Here 4th argument inside Join is used as an inner select to project the required fields,
            /// i.e pulling all the information into a single object so that a single IEnumerable can be returned.
            /// 
            var query2 =
                cars.Join(manufacturers, c => c.Manufacturer,
                        m => m.Name, (c, m) => new
                        {
                            m.Headquarters,
                            c.Name,
                            c.Combined
                        })
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name);

            /// Joining can be done for multiple values i.e creating a join with a composite key.
            /// So lets join car and manufacturer on the basis of name and year, we will see both 
            /// query way and method way.
            /// So here we create anonymous type for comparing multiple fields, in case field names
            /// are not equal we can assign name as shown here, o.w it will throw an error.
            /// Query Syntax for composite key.
            var query3 = from car in cars
                         join manufacturer in manufacturers
                             on new { car.Manufacturer, car.Year }
                                 equals 
                                 new { Manufacturer = manufacturer.Name, manufacturer.Year }
                         orderby car.Combined descending, car.Name ascending
                         select new
                         {
                             manufacturer.Headquarters,
                             car.Name,
                             car.Combined
                         };

            ///Method Syntax for composite key
            var query4 =
               cars.Join(manufacturers, c => new { c.Manufacturer, c.Year },
                       m => new { Manufacturer = m.Name, m.Year }, (c, m) => new
                       {
                           m.Headquarters,
                           c.Name,
                           c.Combined
                       })
               .OrderByDescending(c => c.Combined)
               .ThenBy(c => c.Name);

            foreach (var car in query4.Take(10))
            {
                Console.WriteLine($"{car.Headquarters}  :  {car.Name}  :  {car.Combined}");
            }
        }

        #endregion

        #region Grouping Data

        static void GroupData(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            ///When we group data we need not use select operation
            /// Grouped data member has a key and with the help of that key, inner group
            /// member can be accessed.
            var query1 =
                from car in cars
                group car by car.Manufacturer.ToUpper() into manufacturer
                orderby manufacturer.Key
                select manufacturer;

            //extension method syntax
            var query2 = cars.GroupBy(c => c.Manufacturer.ToUpper())
                            .OrderBy(g => g.Key);

            foreach (var group in query2)
            {
                Console.WriteLine(group.Key);
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Manufacturer}  :  {car.Name}  :  {car.Combined}");
                }
            }
        }

        #endregion

        #region GroupJoin
        static void GroupJoinOperation(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            /// Group join operation helps in performing join as well as grouping of elements in a single 
            /// query and it is quite effective way of doing things.
            var query = from manufacturer in manufacturers
                        join car in cars
                        on manufacturer.Name equals car.Manufacturer
                        into carGroup
                        orderby manufacturer.Headquarters
                        select new
                        {
                            Manufacturer = manufacturer,
                            Cars = carGroup
                        };

            var query1 = manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
                (m, g) => new
                {
                    Manufacturer = m,
                    Cars = g
                })
                .OrderBy(m => m.Manufacturer.Name);

            foreach (var group in query1)
            {
                Console.WriteLine($"{group.Manufacturer.Name}  :  {group.Manufacturer.Headquarters}");
                foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Manufacturer}  :  {car.Name}  :  {car.Combined}");
                }
            }
        }
        #endregion

        #region Get Fuel Efficient Car By Country.
        static void GetFuelEfficientCarByCountry(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            var query = from manufacturer in manufacturers
                        join car in cars
                        on manufacturer.Name equals car.Manufacturer
                        into carGroup
                        select new
                        {
                            Manufacturer = manufacturer,
                            Cars = carGroup
                        }
                        into result
                        group result by result.Manufacturer.Headquarters;

            var query1 = manufacturers.Join(cars, m => m.Name, c => c.Manufacturer,
                (m, c) => new
                {
                    Manufacturer = m,
                    Cars = c
                })
                .GroupBy(m => m.Manufacturer.Headquarters);

            foreach (var group in query)
            {
                Console.WriteLine($"{group.Key}");
                foreach (var car in group.SelectMany(c => c.Cars)
                    .OrderByDescending(c => c.Combined)
                    .Take(3))
                {
                    Console.WriteLine($"\t{car.Manufacturer}  :  {car.Name}  :  {car.Combined}");
                }
            }
        }
        #endregion

        #region Session 5

        #region Multiple Continous Sort

        /// <summary>
        /// Here this method will return the most fuel efficient car.
        /// </summary>
        /// <param name="cars"></param>
        static void MultipleSort(IEnumerable<Car> cars)
        {
            /// Order by is used to sort for the first time and then "ThenBy" 
            /// is used to sort the query multiple times.
            /// This is the method syntax for doing the multiple sort.
            var query = cars.OrderByDescending(c => c.Combined).ThenBy(c => c.Name);

            ///Here is the query syntax way to represent the above query.
            var query1 = from car in cars
                         orderby car.Combined descending, car.Name ascending
                         select car;

            //Here we are taking only top 10 enteries.
            foreach (var car in query1.Take(10))
            {
                Console.WriteLine($"{car.Name,20} : {car.Combined,3}");
            }
        }

        #endregion

        #region "First" and "Last" Method

        private static void FirstMethod(IEnumerable<Car> cars)
        {
            ///First method is interesting method as it has 2 overloads so in one case
            ///where it did not take any argument it just returns the top entry in the list.
            ///In second case it works like "where", i.e it take a func as argument and return bool.
            ///Although they are not interchangeable as "where" returns list of objects and 
            ///"first" returns only the first result which met the criteria.
            ///Example is:

            var query1 = (from car in cars
                          where car.Manufacturer == "BMW" && car.Year == 2016
                          orderby car.Combined descending, car.Name ascending
                          select car).First();
            Console.WriteLine($"{query1.Manufacturer}  :  {query1.Name}  :  {query1.Year}");

            var query2 = cars.OrderByDescending(c => c.Combined)
                                .ThenBy(c => c.Name)
                                .Select(c => c)
                                .First(c => c.Manufacturer == "BMW" && c.Year == 2016);
            Console.WriteLine($"{query2.Manufacturer}  :  {query2.Name}  :  {query2.Year}");

            ///Just like first we have last method it does everthing same as first but in opposite order.
            ///Here it will return the least efficient car from the input with conditions.
            var query3 = cars.OrderByDescending(c => c.Combined)
                                .ThenBy(c => c.Name)
                                .Select(c => c)
                                .Last(c => c.Manufacturer == "BMW" && c.Year == 2016);
            Console.WriteLine($"{query3.Manufacturer}  :  {query3.Name}  :  {query3.Year}");

            ///                                 (**IMPORTANT)
            /// Along with first and last we have "FirstOrDefault" and "LastOrDefault".
            /// The difference between default and normal is that, the query might throw exception when
            /// certain criteria is not met, so with the help of default method we can set the object, to their 
            /// default values, i.e for class object it will be null for integers it will be 0, etc.
            var query4 = cars.OrderByDescending(c => c.Combined)
                                .ThenBy(c => c.Name)
                                .Select(c => c)
                                .FirstOrDefault(c => c.Manufacturer == "aaa" && c.Year == 2016);
            if (query4 != null)
                Console.WriteLine($"{query4.Manufacturer}  :  {query4.Name}  :  {query4.Year}");
            else
                Console.WriteLine("There is an exception.");
        }

        #endregion

        #region Any, All, Contains
        /// <summary>
        /// All the three methods Any, All, Contains return boolean value.
        /// </summary>
        /// <param name="cars"></param>
        static void StudyBoolReturnMethod(IEnumerable<Car> cars)
        {
            /// Any() Method without argument checks if there are any value inside the list
            /// Whereas with arg it checks for the condition and return true if it finds any condition true.
            var resultAny1 = cars.Any();
            var resultAny2 = cars.Any(c => c.Manufacturer == "Ford");

            /// All() method checks if all the values inside an enumerable follows the particular
            /// condition and if it found any condition not true it simply returns the false.
            var resultAll1 = cars.All(c => c.Manufacturer == "Ford");

            ///Contains check if any object is available in the list and return true if available 
            ///else false.
            var car = new Car();
            var resultContains = cars.Contains(car);
        }

        #endregion

        #region Projection Data with select

        static void Projection(IEnumerable<Car> cars)
        {
            /// With the help of projection we can ignore the unnecessary data.
            /// For example: If we have a table with 1000 columns and only 3 columns are of use to us.
            /// Then we can apply the projection so as to store only required columns.
            /// It is similar to the select projection in sql.
            var result = cars.Select(c => new
            {
                c.Manufacturer,
                c.Name,
                c.Combined
            });

            //foreach (var car in result.Take(10))
            //{
            //    Console.WriteLine(car.Manufacturer+" : "+car.Name);
            //}

            ///We can also create anonymous types using var and then with the help of it perform projection.
            ///They are readonly?
            var template = new
            {
                Name = "Venom",
                CarName = "Audi"
            };

            ///Example in a real query.
            var query = from car in cars
                        orderby car.Combined descending, car.Name ascending
                        select new
                        {
                            car.Manufacturer,  // OR Manufacturer = car.Manufacturer
                            car.Name,
                            car.Combined
                        };
        }

        #endregion

        #region Select Many Operator

        static void SelectManyOperator(IEnumerable<Car> cars)
        {
            ///SelectMany is used in cases when there are collections of collections.
            ///SelectMany has an inner loop that helps in iterating for inner records easy.
            ///This may come handy in some cases if not all.
            ///Here we can see name as an IEnumerable of characters so we will access each char using SelectMany.
            var query = cars.SelectMany(c => c.Name);

            foreach (var character in query)
            {
                Console.WriteLine(character);
            }
        }

        #endregion

        #endregion
    }

    #region Car Extension Class
    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var column = line.Split(',');
                yield return new Car
                {
                    Year = Convert.ToInt32(column[0]),
                    Manufacturer = column[1],
                    Name = column[2],
                    Displacement = Convert.ToDouble(column[3]),
                    Cylinders = Convert.ToInt32(column[4]),
                    City = Convert.ToInt32(column[5]),
                    Highway = Convert.ToInt32(column[6]),
                    Combined = Convert.ToInt32(column[7])
                };
            }
        }
    }

    #endregion
}
