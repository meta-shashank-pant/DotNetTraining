using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpIntermediate
{
    partial class PartialClass
    {
        /// <summary>
        /// This method is used to print Hello on the screen along name.
        /// </summary>
        /// <param name="name">String name to display.</param>
        public void SayHello(string name)
        {
            Console.WriteLine("Hello "+name);
        }
    }

    partial class PartialClass
    {
        /// <summary>
        /// This method is used to print Bye on the screen along name.
        /// </summary>
        /// <param name="name">String name to display.</param>
        public void SayBye(string name)
        {
            Console.WriteLine("Bye "+name);
        }
    }
}
