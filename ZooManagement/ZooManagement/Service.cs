using System;
using System.Collections.Generic;
using System.Text;

namespace ZooManagement
{
    class Service
    {
        /// <summary>
        /// Add an animal to cage.
        /// </summary>
        /// <param name="animal">Animal object</param>
        /// <returns>true if operation is successful, false otherwise.</returns>
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

        /// <summary>
        /// Find if cage is available in particular zone for that animal.
        /// </summary>
        /// <param name="animal">Animal object</param>
        /// <returns>Cage object is avialble, null otherwise.</returns>
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


        /// <summary>
        /// Remove an animal from cage.
        /// </summary>
        /// <param name="animal">Animal object</param>
        /// <returns>true if operation is successful, false otherwise.</returns>
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

        /// <summary>
        /// Add the cage in an Zone for specific animal.
        /// </summary>
        /// <param name="zone">Zone object</param>
        /// <param name="cage">Cage object</param>
        /// <returns>true if cage is avialable, false otherwise</returns>
        public static bool AddCage(Zone zone, Cage cage)
        {
            return zone.AddCage(cage);
        }
    }
}
