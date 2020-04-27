using System;

namespace CSharpAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var garbageCollection = new GarbageCollection())
            {
                string result = garbageCollection.GetDate();
                Console.WriteLine("Date is " + result);
            }
        }
    }
}
