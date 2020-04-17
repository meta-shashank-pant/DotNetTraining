using System;
using System.Linq;
using System.IO;

namespace Exercise
{
    class Program
    {
        //Single line comment.

        /*Multiple
         Line
         Comment.*/

        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");

            //Object creation
            /*Class1 obj = new Class1();
            obj.Statement();*/

            //Subject class
            /*Subject sub1 = new Subject();
            sub1.Name = "Data Structure";
            sub1.Marks = 70;
            sub1.Code = "DS123";
            sub1.FavTopic = "Graphs";
            Console.WriteLine("Favourite Topic: "+sub1.FavTopic);
            sub1.Status();*/

            //Inheritance and Polymorphism Testing
            /* Animal animal = new Animal();
             Lion lion = new Lion();
             Giraffe giraffe = new Giraffe();
             animal.Voice();
             lion.Voice();
             giraffe.Voice(); */

            //Abstract Class
            /* Car car = new Car();
             car.EnginePower();
             car.VehicleInfo("Audi",4); */

            //Interface
            /* ScienceBook sb = new ScienceBook();
             sb.BookTitle();
             sb.BookPrice(); */

            //enums test
            /*Status status = Status.Success;
            if (status.Equals(Status.Success))
            {
                Console.WriteLine("Your status is success.");
            } */

            //Files();
            //ExceptionHandling();

        }

        static void ExceptionHandling()
        {
            int x = 5, y = 0;
            try
            {
                Console.WriteLine(x/y);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Values of x and y are: "+x+" and "+y);
            }
        }

        static void Files()
        {
            //read text from a file
            string readText = File.ReadAllText("DemoText.txt");
            Console.WriteLine(readText);

            string writeText = " How are you?";

            //write text to the file by removing all the text before.
            //File.WriteAllText("DemoText.txt", writeText);

            //Append to the end of the file.
            File.AppendAllText("DemoText.txt", writeText);


        }

        public static string Concat(string str1, string str2)
        {
            return str1 + str2;
        }

        public static void Greeting(String name, int age)
        {
            Console.WriteLine("Hello "+name+", you are "+age+".");
        }

        public static void Greeting(String name)
        {
            Console.WriteLine("Hello "+name);
        }

        public static void Arrays()
        {
            //create an array
            int[] numbers = { 12, 9, 7, 4, 1 };
            string[] subject = {"Maths", "Physics", "Geography", "History"};

            //Accessing the value
            Console.WriteLine(subject[0]);

            //Changing an array item
            subject[3] = "Geology";
            Console.WriteLine(subject[3]);

            //Array length
            Console.WriteLine(subject.Length);

            //Loop through an array
            for(int i=0; i<subject.Length; i++)
            {
                Console.WriteLine(subject[i]);
            }

            //foreach loop
            foreach (int i in numbers)
                Console.WriteLine(i);

            //Sort the array
            Array.Sort(numbers);
            Array.Sort(subject);

            //using System.Linq methods
            Console.WriteLine(numbers.Max());
            Console.WriteLine(numbers.Min());
            Console.WriteLine(numbers.Sum());
        }

        public static void ContinueBreakStatement()
        {
            //Does not print for value equal to 3 and break if value is more than 5
            for(int i = 0; i < 10; i++)
            {
                if (i == 3)
                    continue;
                Console.WriteLine("i = "+i);
                if (i >= 5)
                    break;
            }
        }

        public static void ForLoop()
        {
            String name = "Shashank";
            //Normal for loop
            for(int i = 0; i < name.Length; i++)
            {
                Console.Write(name[i]+" ");
            }
            Console.WriteLine();
            //print even numbers in range 10
            Console.Write("Even numbers in range 10: ");
            for(int i = 2; i <= 10; i += 2)
            {
                Console.Write(i+" ");
            }
            Console.WriteLine();
            string[] names = {"John", "Jeremy", "Shelly"};
            foreach(string person in names)
            {
                Console.WriteLine(person);
            }
        }

        public static void WhileLoop()
        {
            int num = 5;
            //While Loop
            while(num > 0)
            {
                Console.WriteLine("WHILE: Current num is "+ num);
                num--;
            }

            //Do While loop
            num = 5;
            do
            {
                Console.WriteLine("DO WHILE: Current num is " + num);
                num--;
            } while (num > 0);
        }

        public static void SwitchStatement()
        {
            int choice = 3;

            switch (choice)
            {
                case 1: 
                    Console.WriteLine("Choice is 1.");
                    break;
                case 2:
                    Console.WriteLine("Choice is 2.");
                    break;
                case 3:
                    Console.WriteLine("Choice is 3.");
                    break;
                default:
                    Console.WriteLine("Choice is other than 1, 2 and 3.");
                    break;
            }
        }

        public static void IfElseStatement()
        {
            //if statement
            if(20 > 18)
            {
                Console.WriteLine("20 is greater than 18.");
            }

            //if else statement
            int x = 2, y = 3;
            if(x > y)
            {
                Console.WriteLine("X is greater than Y");
            }
            else
            {
                Console.WriteLine("Y is greater than X");
            }

            //else if statement
            int z = 5;
            if(x > y && x > z)
            {
                Console.WriteLine("X is greatest.");
            }else if(y > x && y > z)
            {
                Console.WriteLine("Y is greatest.");
            }
            else
            {
                Console.WriteLine("Z is greatest.");
            }

            //Ternary Operator
            int max = (x > y) ? x : y;
            Console.WriteLine("Max using ternary operator: "+ max);
        }

        public static void Booleans()
        {
            bool isCSharpFun = true;
            Console.WriteLine(isCSharpFun);
            Console.WriteLine(10 > 9);
            Console.WriteLine(10 == 9);
        }

        public static void Variables()
        {
            //C# Variables
            int myNumber = 50;
            string myName = "John";
            int x = 5;
            int y = 10;
            Console.WriteLine(x + y);
            int z = x + y;
            Console.WriteLine(z);
            //intializing variables in comma separated list
            int x1 = 5, y1 = 6, z1 = 50;
            Console.WriteLine(x1 + y1 + z1);
        }

        public static void DataTypes()
        {
            //C# Data Types
            int myNum = 9;
            double myDoubleNum = 8.99;
            char myLetter = 'C';
            bool myBoolean = true;
            string myText = "Hello World";
            long myLong = 15000000000L;
            float myFloat = 5.75F;
            double d1 = 12e4;
        }

        public static void TypeCasting()
        {
            //C# Type Casting
            //Automatic type casting
            int abc = 23;
            double new_abc = abc;
            Console.WriteLine("Implicit type casting from int to double: Integer = " + abc + " Double = " + new_abc);
            //Explicit type casting
            double demo = 23.45;
            int demoTC = (int)demo;
            Console.WriteLine("Explicit type casting from double to int: Integer = " + demoTC + " Double = " + demo);

            //Type conversion methods.
            int i_num = 15;
            double d_num = 23.56;
            bool is_true = true;
            Console.WriteLine(Convert.ToInt32(d_num));
            Console.WriteLine(Convert.ToString(is_true));
            Console.WriteLine(Convert.ToDouble(i_num));
            Console.WriteLine(Convert.ToString(i_num));
        }

        public static void Operators()
        {

            //Operators in C#
            double num1 = 10;
            double num2 = 6;
            Console.WriteLine("\nOPEARTORS in C#\nNumber 1 = " + num1 + "\tNumber 2 = " + num2);
            Console.WriteLine("Addition: " + (num1 + num2));
            Console.WriteLine("Substraction: " + (num1 - num2));
            Console.WriteLine("Multiplication: " + (num1 * num2));
            Console.WriteLine("Division: " + (num1 / num2));
            Console.WriteLine("Modulus: " + (num1 % num2));
            Console.WriteLine("Increment number 1: " + (++num1));
            Console.WriteLine("Decrement number 2: " + (--num2));
        }

        public static void MathClass()
        {
            // C# Math class methods
            Console.WriteLine(Math.Max(5, 10));
            Console.WriteLine(Math.Min(5, 10));
            Console.WriteLine(Math.Sqrt(25));
            Console.WriteLine(Math.Abs(-10));
            Console.WriteLine(Math.Round(45.4545));
        }

        public static void StringOperation()
        {
            //C# Strings
            string txt = "Hello, My name is shashank pant.";
            //String Length
            Console.WriteLine("Lenght of txt is " + txt.Length);
            //String Concatination
            string fname = "Shashank";
            string lname = "Pant";
            String name = fname + lname;
            Console.WriteLine(name);
            //Sting Interpolation
            string name1 = $"My name is {fname} {lname}";
            Console.WriteLine(name1);
            //Accessing String
            string myString = "Hello";
            Console.WriteLine(myString + " has " + myString[0] + " value at index 0.");
            //IndexOf method (we can pass char or string, for string it will return the starting position of the given string in main string)
            Console.WriteLine(myString.IndexOf("ll"));
            //Substring method: It will return the string from the given index till the end of the string.
            string sub_string = myString.Substring(2);
            Console.WriteLine("Substring method: It will return the string from the given index till the end of the string. Here " + sub_string);
            //Escaping special character
            Console.WriteLine("Escaping special character like ( \\ , \", \' )");

        }

        public static void Addition()
        {
            //Reading input from the user. 

            Console.WriteLine("\nAdditon of 2 numbers");
            Console.Write("Enter 1st number: ");
            string arg1 = Console.ReadLine();
            Console.Write(" Enter 2nd number: ");
            string arg2 = Console.ReadLine();
            Console.WriteLine("Answer is: " + (Convert.ToInt32(arg1) + Convert.ToInt32(arg2)));

        }

    }
}
