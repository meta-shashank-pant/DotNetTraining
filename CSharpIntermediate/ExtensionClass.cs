using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpIntermediate
{
    /// <summary>
    /// This is the static class used for making extension methods.
    /// This extension method will only work in this namespace only (by default).
    /// Extension methods allow you to inject additional methods without modifying,
    /// deriving or recompiling the original class, struct or interface.
    /// </summary>
    static class ExtensionClass
    {
        /// <summary>
        /// This is an extension method which is used for removing whitespaces from the given string text.
        /// </summary>
        /// <param name="text">Input string from which whitespace is to be removed.</param>
        /// <returns>An string with no whitespace.</returns>
        public static string RemoveSpace(this string text)
        {
            StringBuilder result = new StringBuilder();
            foreach(char i in text)
            {
                if(!i.Equals(' '))
                {
                    result.Append(i);
                }
            }
            return result.ToString();
        }
    }
}
