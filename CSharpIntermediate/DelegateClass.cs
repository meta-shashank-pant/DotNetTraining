using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpIntermediate
{
    delegate int DelegateOperation(int value1, int value2);
    class DelegateClass
    {
        public static int FindMax(int value1, int value2)
        {
            Console.Write("Max Value = ");
            return (value1 > value2) ? value1 : value2;
        }

        public static int FindMin(int value1, int value2)
        {
            Console.Write("Min Value = ");
            return (value1 < value2) ? value1 : value2;
        }

        public static int PrintMaxException(int value1, int value2)
        {
            int value = (value1 > value2) ? value1 : value2;
            Console.WriteLine("Max Value = " + value);
            throw new Exception("Error");
            return 0;
        }

        public static int PrintMax(int value1, int value2)
        {
            int value = (value1 > value2) ? value1 : value2;
            Console.WriteLine("Max Value = " + value);            
            return 0;
        }

        public static int PrintMin(int value1, int value2)
        {
            int value = (value1 < value2) ? value1 : value2;
            Console.WriteLine("Min Value = " + value);
            return 0;
        }
    }
}
