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

        //Getter and Setter
        public string AnimalCategory
        {
            get { return animalCategory; }
        }

        public List<Cage> Cages
        {
            get { return cages; }
        }
        
        //Add cage
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

        public bool hasPark()
        {
            return isPark;
        }

        public bool hasCanteen()
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

        //Delete animal from the list
        public void DeleteAnimalFromList(Animal animal)
        {
            animals.Remove(animal);
            currentNumberOfAnimals--;
        }

        //Getter and Setters
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

        //Add animal to cage
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
