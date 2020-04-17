using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise
{
    //Normal class, Class Members 
    class Class1
    {
        //Class Member
        string color = "Yellow";
        string shape = "Triangle";

        public void Statement()
        {
            Console.WriteLine("You like "+shape+" which is "+color+" in color.");
        }

    }

    //Access modifiers, Constructor and Property(Get and Set)
    class Subject
    {
        // private access modifier.
        private string name;
        private int marks;
        private string code;

        //Automatic Properties(Short Hand)
        public string FavTopic
        {
            get; set;
        }

        public Subject(string name, int marks, string code)
        {
            this.name = name;
            this.marks = marks;
            this.code = code;
        }

        //Default constructor
        public Subject() { }
        
        //Properties(Getter and Setter)
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Marks
        {
            get { return marks; }
            set { marks = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public void Status()
        {
            Console.WriteLine("Subject Name: "+name+"\nMarks: "+marks+"\nCode: "+code);
        }

    }

    //Inheritance and Polymorphism
    class Animal
    {
        protected string type = "Land";

        public virtual void Voice()
        {
            Console.WriteLine("Here come the voice of the animal.");
        }
    }

    class Lion : Animal
    {
        string name = "Alex";

        public override void Voice()
        {
            Console.WriteLine("Lion is a "+ type +" animal, his name is "+name+" and he roars loudly.");
        }
    }

    class Giraffe : Animal
    {
        string name = "Jeremy";

        public override void Voice()
        {
            Console.WriteLine("Giraffe is a " + type + " animal, his name is " + name + " and he hum slowly.");
        }
    }
    
    //Abstract Class
    abstract class Vehicle
    {
        string name = null;
        int no_of_tyre = 0;

        public abstract void EnginePower();

        public void VehicleInfo(string name, int no_of_tyre)
        {
            this.name = name;
            this.no_of_tyre = no_of_tyre;

            Console.WriteLine("This Vehicle name is "+name+" and it has "+no_of_tyre+" tyres.");
        }
    }

    class Car : Vehicle
    {
        public override void EnginePower()
        {
            Console.WriteLine("Engine Power is 1100.");
        }
        
    }

    //Interface
    interface Book
    {
        void BookTitle();
        void BookPrice();
    }

    class ScienceBook : Book
    {
        public void BookPrice()
        {
            Console.WriteLine("Book price is 500.");
        }

        public void BookTitle()
        {
            Console.WriteLine("Title of the book is \"Beyond Infinity\"");
        }
    }

    //enums
    enum Status{
        Success, Failure, Error
    }

}
