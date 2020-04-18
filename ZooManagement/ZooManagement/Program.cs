﻿using System;
using System.Collections.Generic;

namespace ZooManagement
{
    class Program
    {
        //This is the list of type Animals for faster access.
        public static List<Animal> DbAnimals = new List<Animal>(); 
        static void Main(string[] args)
        {
            PreBuildData();

            while (true)
              {
                  try
                  {
                      Console.WriteLine("\nEnter Your Choice:\n1. Add Animal\n2. Remove Animal\n3. Add a Cage\n4. Display Animal\n5. Exit");
                      int choice = Convert.ToInt32(Console.ReadLine());
                      bool status = Controller.StartSystem(choice);
                      if (!status)
                          break;
                  }
                  catch (Exception e)
                  {
                      Console.WriteLine(e.Message);
                  }
              }

              Console.WriteLine("Successfully Exit."); 

        }

        //This method will return animal object corresponding to given id.
        public static Animal GetAnimalById()
        {
            DisplayAnimals();
            Console.Write("Enter animal id: ");
            int animalId = TakeIntegerInput();

            foreach(Animal animal in DbAnimals)
            {
                if (animal.IdNum == animalId)
                    return animal;
            }

            return null;

        }

        //This method is used to display the zoo on the basis of zones.
        public static void DisplayAnimals()
        {
            int i = 1, j = 1;
            Console.WriteLine("Id\t\tName\t\tAge\t\tWeight\t\tSound");
            Console.WriteLine("--------------------------------------------------------------------------");
            foreach (Zone zone in Zone.zones)
            {
                Console.WriteLine("\nWelcome to Zone" + (i++) + "(" + zone.AnimalCategory + ")");
                foreach (Cage cage in zone.Cages)
                {
                    if(!(cage.Animals.Count == 0))
                    {
                        Console.WriteLine("Cage" + (j++) + " (Capacity = "+cage.AnimalCapacity+", Animal Type = "+cage.TypeOfAnimal+"): ");
                        Console.WriteLine("--------------------------------------------------------------------------");
                    }
                    foreach(Animal animal in cage.Animals)
                    {
                        Console.WriteLine(animal.IdNum + "\t\t" + animal.Name + "\t\t" + animal.Age + "\t\t" + animal.Weight + "\t\t" + animal.getSound());
                    }
                }
            }
        }

        //This struct holds the value of name, age and weight of animal
        public struct AnimalAttributes
        {
            public string name;
            public int age;
            public double weight;
        }

        //This method is used to return animal object to controller
        public static Animal CreateAnimalObject()
        {
            try
            {
                invalidOption:
                Console.WriteLine("Select an option: \n1. Mammal\n2. Reptile\n3. Bird\n4. Aquatic Animal\n5. Go Back");
                int categoryChoice = Convert.ToInt32(Console.ReadLine());
                int animalChoice = 0;
                AnimalAttributes animalAttributes;
                switch (categoryChoice)
                {
                    case 1:
                        wrongMammalChoice:
                        Console.WriteLine("Select an option:\n1. Lion\n2. Elephant");
                        animalChoice = TakeIntegerInput();
                        if (!(animalChoice == 1 || animalChoice == 2))
                            goto wrongMammalChoice;
                        animalAttributes = SetAnimalObject();
                        if (animalChoice == 1)
                            return new Lion(animalAttributes.name, animalAttributes.age, animalAttributes.weight);
                        else
                            return new Elephant(animalAttributes.name, animalAttributes.age, animalAttributes.weight);

                    case 2:
                        wrongReptileChoice:
                        Console.WriteLine("Select an option:\n1. Crocodile\n2. Lizard");
                        animalChoice = TakeIntegerInput();
                        if (!(animalChoice == 1 || animalChoice == 2))
                            goto wrongReptileChoice;
                        animalAttributes = SetAnimalObject();
                        if (animalChoice == 1)
                            return new Crocodile(animalAttributes.name, animalAttributes.age, animalAttributes.weight);
                        else
                            return new Lizard(animalAttributes.name, animalAttributes.age, animalAttributes.weight);

                    case 3:
                        wrongBirdChoice:
                        Console.WriteLine("Select an option:\n1. Peacock\n2. Sparrow");
                        animalChoice = TakeIntegerInput();
                        if (!(animalChoice == 1 || animalChoice == 2))
                            goto wrongBirdChoice;
                        animalAttributes = SetAnimalObject();
                        if (animalChoice == 1)
                            return new Peacock(animalAttributes.name, animalAttributes.age, animalAttributes.weight);
                        else
                            return new Sparrow(animalAttributes.name, animalAttributes.age, animalAttributes.weight);

                    case 4:
                        wrongAquaticChoice:
                        Console.WriteLine("Select an option:\n1. Fish\n2. Seal");
                        animalChoice = TakeIntegerInput();
                        if (!(animalChoice == 1 || animalChoice == 2))
                            goto wrongAquaticChoice;
                        animalAttributes = SetAnimalObject();
                        if (animalChoice == 1)
                            return new Fish(animalAttributes.name, animalAttributes.age, animalAttributes.weight);
                        else
                            return new Seal(animalAttributes.name, animalAttributes.age, animalAttributes.weight);
                    case 5:
                        return null;
                    default:
                        Console.WriteLine("Invalid Option!");
                        goto invalidOption;
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        //This method contains the common code from CreateAnimalObject
        public static AnimalAttributes SetAnimalObject()
        {
            string name;
            int age;
            double weight;


            wrongName:
            Console.Write("Enter name: ");
            name = TakeStringInput();
            if (name == null)
                goto wrongName;

            wrongAge:
            Console.Write("Enter age: ");
            age = TakeIntegerInput();
            if (age == -1)
                goto wrongAge;

            wrongWeight:
            Console.Write("Enter weight: ");
            weight = TakeDoubleInput();
            if (weight == -1)
                goto wrongWeight;

            AnimalAttributes animalAttributes = new AnimalAttributes();
            animalAttributes.name = name;
            animalAttributes.age = age;
            animalAttributes.weight = weight;

            return animalAttributes;

        }

        //Input for various data types: int, string, double.
        public static string TakeStringInput()
        {
            try
            {
                string output = Console.ReadLine();
                return output;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static int TakeIntegerInput()
        {
            try
            {
                int output = Convert.ToInt32(Console.ReadLine());
                return output;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public static double TakeDoubleInput()
        {
            try
            {
                double output = Convert.ToDouble(Console.ReadLine());
                return output;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        //Predefined data
        static void PreBuildData()
        {
            //By default cages and animal enteries for easy use:
            //Creating pre defined 5 zones, Zone1, Zone2, Zone3, Zone4, Zone5.
            Zone zone1 = new Zone("Mammal", 3, true, true);
            Zone zone2 = new Zone("Reptile", 2, true, false);
            Zone zone3 = new Zone("Aquatic", 2, false, true);
            Zone zone4 = new Zone("Bird", 3, true, false);
            Zone zone5 = new Zone("Mammal", 2, false, false);

            //Creating cages byb default.
            Cage cage1 = new Cage("Lion", 2);
            Cage cage2 = new Cage("Elephant", 2);
            Cage cage3 = new Cage("Lion", 2);
            Cage cage4 = new Cage("Crocodile", 2);
            Cage cage5 = new Cage("Lizard", 2);
            Cage cage6 = new Cage("Fish", 2);
            Cage cage7 = new Cage("Seal", 2);
            Cage cage8 = new Cage("Peacock", 2);
            Cage cage9 = new Cage("Sparrow", 2);
            Cage cage10 = new Cage("Peacock", 2);
            Cage cage11 = new Cage("Elephant", 2);
            Cage cage12 = new Cage("Lion", 2);

            //Add cages in zone.
            zone1.AddCage(cage1);
            zone1.AddCage(cage2);
            zone1.AddCage(cage3);
            zone2.AddCage(cage4);
            zone2.AddCage(cage5);
            zone3.AddCage(cage6);
            zone3.AddCage(cage7);
            zone4.AddCage(cage8);
            zone4.AddCage(cage9);
            zone4.AddCage(cage10);
            zone5.AddCage(cage11);
            zone5.AddCage(cage12);

            //Now, Some objects of Animal for easy testing.
            Animal lion1 = new Lion("Alex", 12, 23.45);
            Animal elephant1 = new Elephant("Elle", 12, 23.45);
            Animal crocodile1 = new Crocodile("Croco", 12, 23.45);
            Animal lizard1 = new Lizard("Liza", 12, 23.45);
            Animal peacock1 = new Peacock("Peacky", 12, 23.45);
            Animal seal1 = new Seal("Sia", 12, 23.45);

            //Adding animals to the cage
            Service.AddAnimal(lion1);
            Service.AddAnimal(elephant1);
            Service.AddAnimal(crocodile1);
            Service.AddAnimal(lizard1);
            Service.AddAnimal(peacock1);
            Service.AddAnimal(seal1);


            //  Console.WriteLine(lion1.GetType().Name);
            //  Console.WriteLine(lion1.GetType().BaseType);
        }
    }
}
