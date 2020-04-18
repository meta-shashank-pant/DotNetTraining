using System;
using System.Collections.Generic;
using System.Text;

namespace ZooManagement
{
    class Controller
    {

        public static bool StartSystem(int choice)
        {
            bool status;
            switch(choice){
                case 1:
                    //Add animal
                    Animal animal = Program.CreateAnimalObject();
                    if (animal == null)
                        return true;
                    status = Service.AddAnimal(animal);
                    if (!status)
                        Console.WriteLine("Out on capacity, add a cage.");
                    return true;
                case 2:
                    //Remove Animal
                    animal = Program.GetAnimalById();
                    if (animal == null)
                        Console.WriteLine("Animal with given Id does not exist.");
                    else
                        Service.DeleteAnimal(animal);
                    return true;
                case 3:
                    //Add a cage
                    return true;
                case 4:
                    //Display animal
                    Program.DisplayAnimals();
                    return true;
                case 5:
                    //Exit
                    return false;
                default:
                    return true;
                    
            }
        }
    }
}
