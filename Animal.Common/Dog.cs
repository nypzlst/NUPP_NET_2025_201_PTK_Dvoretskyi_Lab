using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal.Common
{
    public class Dog : Animal
    {
        static Dog()
        {
            EnergyLevel = "low"; 
        }



        public Dog(string name, string classOfAnimal, string orderOfAnimal, string nickname)
        {
            Name = name;    
            ClassOfAnimal = classOfAnimal;
            OrderOfAnimal = orderOfAnimal;
            Nickname = nickname;
        }


        public Dog()
        {
            Name = "Cur dog";
        }


        public string? Breed { get; set; }
        public string? TrainingLevel { get; set; }
        public static string? EnergyLevel { get; set; }
        public string? Nickname { get; set; }

        public void Bark()
        {
            Console.WriteLine("woof");
        }

        public void ReactToGettingOwner(Owner owner)
        {
            Console.WriteLine($"Собака {Nickname} счастлива! Теперь у неё есть хозяин {owner.FullName}.");
            Bark();
        }
    }

    public static class DogExtensions
    {
        public static void BarkLoud(this Dog dog)
        {
            Console.WriteLine($"{dog.Nickname} bark");
        }
    }
}
