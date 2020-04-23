using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CSharpIntermediate
{
    /// <summary>
    /// This is an generic class, which has one method for comparing two params.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class GenericClass <T>
    {
        readonly string equal = "EQUAL";
        readonly string unequal = "UNEQUAL";
        /// <summary>
        /// This is an generic method using two type params and then comparing them.
        /// </summary>
        /// <param name="value1">This is the first value.</param>
        /// <param name="value2">This is the second value.</param>
        /// <returns>This method will return "EQUAL" is both params are same otherwise it return "UNEQUAL".</returns>
        public string Compare(T value1, T value2)
        {
            if(value1.Equals(value2))
            {
                return equal;
            }
            else
            {
                return unequal;
            }
        }
    }

    /// <summary>
    /// This is an generic class acting as an collection of given type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class GenericCollections<T> : IEnumerable<T>
    {
        List<T> list = new List<T>();

        /// <summary>
        /// This generic method is used in adding the value to the defined list object.
        /// </summary>
        /// <param name="value">This value is added to the list.</param>
        public void AddValue(T value)
        {
            list.Add(value);
        }

        /// <summary>
        /// This generic method is used to find if the parameter is present in the list or not.
        /// </summary>
        /// <param name="value">This value is searched in the list.</param>
        /// <returns>If param is found in the list, it return true otherwise false.</returns>
        public bool SearchValue(T value)
        {
            foreach(T element in list)
            {
                if (element.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Below method are the overrided method from the IEnumerable interface.
        /// These method helps in performing foreach operation on object.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
