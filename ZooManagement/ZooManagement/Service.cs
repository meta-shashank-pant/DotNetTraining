using System;
using System.Collections.Generic;
using System.Text;

namespace ZooManagement
{
    class Service
    {
        //This method is used to add an animal to cage.
        public static bool AddAnimal(Animal animal)
        {
            try
            {
                Cage cage = CageForAnimal(animal);
                if (cage == null)
                    return false;
                cage.AddAnimalToList(animal);
                Program.DbAnimals.Add(animal);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return true;
        }

        //This method will find if cage is available in particular zone for that animal.
        public static Cage CageForAnimal(Animal animal)
        {
            foreach (Zone zone in Zone.zones)
            {
                if (!Convert.ToString(animal.GetType().BaseType).Contains(zone.AnimalCategory))
                    continue;

                foreach (Cage cage in zone.Cages)
                {
                    if (cage.TypeOfAnimal == Convert.ToString(animal.GetType().Name))
                    {
                        if (cage.AnimalCapacity > cage.CurrentNumberOfAnimals)
                        {
                            //Console.WriteLine("Cage founded...");
                            return cage;
                        }
                    }
                }
            }
            //Console.WriteLine("Cage not founded...");
            return null;
        }

        //This method is used to remove an animal from cage.
        public static bool DeleteAnimal(Animal animal)
        {
            foreach (Zone zone in Zone.zones)
            {
                if (!Convert.ToString(animal.GetType().BaseType).Contains(zone.AnimalCategory))
                    continue;

                foreach (Cage cage in zone.Cages)
                {
                    if (cage.TypeOfAnimal == Convert.ToString(animal.GetType().Name))
                    {
                        foreach (Animal randomAnimal in cage.Animals)
                        {
                            if (randomAnimal.IdNum == animal.IdNum)
                            {
                                cage.DeleteAnimalFromList(animal);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
