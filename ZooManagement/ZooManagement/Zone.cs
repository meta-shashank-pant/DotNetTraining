using System;
using System.Collections.Generic;
using System.Text;

namespace ZooManagement
{
    class Zone
    {
        public static List<Zone> zones = new List<Zone>();
        string animalCategory;
        int numberOfCages;
        static int currentNumber = 0;
        List<Cage> cages;
        bool isPark, isCanteen;

        public Zone(string animalCategory, int numberOfCages, bool isPark, bool isCanteen)
        {
            this.animalCategory = animalCategory;
            this.numberOfCages = numberOfCages;
            this.isCanteen = isCanteen;
            this.isPark = isPark;
            cages = new List<Cage>();
            zones.Add(this);
        }

        public string AnimalCategory
        {
            get { return animalCategory; }
        }

        public List<Cage> Cages
        {
            get { return cages; }
        }
        
        /// <summary>
        /// Add cage object to the cage list inside zone.
        /// </summary>
        /// <param name="cage">Cage object</param>
        /// <returns>true always</returns>
        public bool AddCage(Cage cage)
        {
            if(currentNumber < numberOfCages)
            {
                cages.Add(cage);
                currentNumber++;
                return true;
            }
           
            cages.Add(cage);
            currentNumber++;
            numberOfCages++;
            return true;
            
        }

        /// <summary>
        /// Check Park in the Zone.
        /// </summary>
        /// <returns>true is available, false otherwise</returns>
        public bool HasPark()
        {
            return isPark;
        }

        /// <summary>
        /// Check Canteen in the zone.
        /// </summary>
        /// <returns>true is available, false otherwise</returns>
        public bool HasCanteen()
        {
            return isCanteen;
        }
    }

    class Cage
    {
        string typeOfAnimal;
        int animalCapacity;
        int currentNumberOfAnimals = 0;
        List<Animal> animals;
        public Cage(string typeOfAnimal, int animalCapacity)
        {
            this.typeOfAnimal = typeOfAnimal;
            this.animalCapacity = animalCapacity;

            animals = new List<Animal>();
        }

        /// <summary>
        /// Delete animal from the list
        /// </summary>
        /// <param name="animal">Animal object</param>
        public void DeleteAnimalFromList(Animal animal)
        {
            animals.Remove(animal);
            currentNumberOfAnimals--;
        }

        
        public string TypeOfAnimal
        {
            get { return this.typeOfAnimal; }
        }

        public int AnimalCapacity
        {
            get { return this.animalCapacity; }
        }

        public int CurrentNumberOfAnimals
        {
            get { return this.currentNumberOfAnimals; }
        }

        public List<Animal> Animals
        {
            get { return animals; }
        }

        /// <summary>
        /// Add animal to cage
        /// </summary>
        /// <param name="animal">Animal object</param>
        /// <returns>true if current number of animal is less than capacity, false otherwise.</returns>
        public bool AddAnimalToList(Animal animal)
        {
            if(currentNumberOfAnimals < animalCapacity)
            {
                animals.Add(animal);
                currentNumberOfAnimals++;
                return true;
            }
            return false;
        }
    }
}
