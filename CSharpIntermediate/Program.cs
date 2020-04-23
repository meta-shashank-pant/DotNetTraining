using System;

namespace CSharpIntermediate
{
    class Program
    {
        static void Main(string[] args)
        {
            //Partial Class
            PartialClass pClass = new PartialClass();
            pClass.SayHello("John");
            pClass.SayBye("John");
            Console.WriteLine();

            //Compare Generic class
            GenericClass<int> genericInteger = new GenericClass<int>();
            string resultInt = genericInteger.Compare(23, 45);
            Console.WriteLine(resultInt);
            GenericClass<string> genericString = new GenericClass<string>();
            string resultString = genericString.Compare("Hello", "Hello");
            Console.WriteLine(resultString);
            Console.WriteLine();

            //Generic class search element
            GenericCollections<int> intList = new GenericCollections<int>();
            intList.AddValue(1);
            intList.AddValue(2);
            intList.AddValue(3);
            intList.AddValue(4);
            intList.AddValue(5);            
            Console.WriteLine("Value Status: "+intList.SearchValue(42));
            Console.WriteLine();

            //Extension method for removing spaces from the string.
            string text = "Hello how are you?";
            Console.WriteLine("\""+text + "\" without whitespaces: " + text.RemoveSpace());
            Console.WriteLine();

            //Use of delegate
            //Normal use:
            DelegateOperation operation = new DelegateOperation(DelegateClass.PrintMax);
            operation(5, 6);
            operation = new DelegateOperation(DelegateClass.PrintMin);
            operation(5, 6);
            Console.WriteLine();

            //Array of Delegates
            DelegateOperation[] operations =
            {
                new DelegateOperation(DelegateClass.PrintMax),
                new DelegateOperation(DelegateClass.PrintMin)
            };

            for(int i=0; i<operations.Length; i++)
            {
                operations[i](7, 8);
                operations[i](3, 1);
                operations[i](72, 34);
            }
            Console.WriteLine();

            //Multicast Delegate
            DelegateOperation operation1 = DelegateClass.PrintMax;
            operation1 += DelegateClass.PrintMin;
            operation1(2, 3);
            operation1(4, 5);
            Console.WriteLine();

            //Creating list from Multicast Delegate
            DelegateOperation operation2 = DelegateClass.PrintMaxException;
            operation2 += DelegateClass.PrintMin;
            Delegate[] del = operation2.GetInvocationList();
            foreach(DelegateOperation op in del)
            {
                try
                {
                    op(2, 3);
                }catch(Exception)
                {
                    Console.WriteLine("Exception caught.");
                }
            }
            Console.WriteLine();

            //Anonymous Method
            DelegateOperation print = delegate (int val1, int val2) {
                Console.WriteLine("Inside Anonymous method. Value1: {0} and Value2: {1}", val1, val2);
                return 0;
            };

            print(100, 200);
        }
    }
}
