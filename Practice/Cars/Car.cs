using System;
using System.Collections.Generic;
using System.Text;

namespace Cars
{
    public class Car
    {
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public double Displacement { get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }

        internal static Car ParseFromCsv(string line)
        {
            var column = line.Split(',');
            return new Car
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
