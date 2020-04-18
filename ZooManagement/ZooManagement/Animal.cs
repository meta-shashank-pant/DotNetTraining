using System;
using System.Collections.Generic;
using System.Text;

namespace ZooManagement
{
    class Animal
    {
        public static int id = 1;
        protected string name;
        protected int age;
        protected double weight;
        protected int idNum;
        
        //Getters for fields
        public string Name
        {
            get { return name; }
        }

        public int Age
        {
            get { return age; }
        }

        public double Weight
        {
            get { return weight; }
        }

        public int IdNum
        {
            get { return idNum; }
        }

        public virtual string getSound()
        {
            return null;
            
        }
    }

    class Mammal : Animal
    {
        public override string getSound()
        {
            return null;
        }
    }

    class Lion : Mammal
    {
        public Lion(string name, int age, double weight)
        {
            this.idNum = id++;
            this.name = name;
            this.age = age;
            this.weight = weight;
        }

        public override string getSound()
        {
            return "Roar";
        }
    }

    class Elephant : Mammal
    {
        public Elephant(string name, int age, double weight)
        {
            idNum = id++;
            this.name = name;
            this.age = age;
            this.weight = weight;
        }
        
        public override string getSound()
        {
            return "Squeaking";
        }
    }

    class Reptile : Animal
    {
        public override string getSound()
        {
            return null;
        }
    }

    class Crocodile : Reptile
    {
        public Crocodile(string name, int age, double weight)
        {
            idNum = id++;
            this.name = name;
            this.age = age;
            this.weight = weight;
        }

        public override string getSound()
        {
            return "Growls";
        }
    }

    class Lizard : Reptile
    {
        public Lizard(string name, int age, double weight)
        {
            idNum = id++;
            this.name = name;
            this.age = age;
            this.weight = weight;
        }

        public override string getSound()
        {
            return "Squeaks";
        }
    }

    class Bird : Animal
    {
        public override string getSound()
        {
            return null;
        }
    }

    class Peacock : Bird
    {
        public Peacock(string name, int age, double weight)
        {
            idNum = id++;
            this.name = name;
            this.age = age;
            this.weight = weight;
        }

        public override string getSound()
        {
            return "Rustling";
        }
    }

    class Sparrow : Bird
    {
        public Sparrow(string name, int age, double weight)
        {
            idNum = id++;
            this.name = name;
            this.age = age;
            this.weight = weight;
        }

        public override string getSound()
        {
            return "Chirping";
        }
    }

    class Aquatic : Animal
    {
        public override string getSound()
        {
            return null;
        }
    }

    class Fish : Aquatic
    {
        public Fish(string name, int age, double weight)
        {
            idNum = id++;
            this.name = name;
            this.age = age;
            this.weight = weight;
        }

        public override string getSound()
        {
            return "Pops";
        }
    }

    class Seal : Aquatic
    {
        public Seal(string name, int age, double weight)
        {
            idNum = id++;
            this.name = name;
            this.age = age;
            this.weight = weight;
        }

        public override string getSound()
        {
            return "Barks";
        }
    }
}
