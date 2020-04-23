using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpIntermediate
{
    /// <summary>
    /// This is an delegate type have two integer params and integer return type.
    /// </summary>
    /// <param name="value1">Integer value 1.</param>
    /// <param name="value2">Integer value 2.</param>
    /// <returns></returns>
    delegate int DelegateOperation(int value1, int value2);
    class DelegateClass
    {
        /// <summary>
        /// This method is used to print the maximum value among 2 params.
        /// And for additional functionality check, there is throw statement in it.
        /// </summary>
        /// <param name="value1">Integer value 1.</param>
        /// <param name="value2">Integer value 2.</param>
        /// <returns>It returns maximum integer value.</returns>
        public static int PrintMaxException(int value1, int value2)
        {
            int value = (value1 > value2) ? value1 : value2;
            Console.WriteLine("Max Value = " + value);
            if(value == 0)
                throw new Exception("Error");
            return value;
        }

        /// <summary>
        /// This method is used to print the maximum value among 2 params.
        /// </summary>
        /// <param name="value1">Integer value 1.</param>
        /// <param name="value2">Integer value 2.</param>
        /// <returns>It returns maximum integer value.</returns>
        public static int PrintMax(int value1, int value2)
        {
            int value = (value1 > value2) ? value1 : value2;
            Console.WriteLine("Max Value = " + value);            
            return value;
        }

        /// <summary>
        /// This method is used to print the minimum value among 2 params.
        /// </summary>
        /// <param name="value1">Integer value 1.</param>
        /// <param name="value2">Integer value 2.</param>
        /// <returns>It returns maximum integer value.</returns>
        public static int PrintMin(int value1, int value2)
        {
            int value = (value1 < value2) ? value1 : value2;
            Console.WriteLine("Min Value = " + value);
            return value;
        }
    }
}
