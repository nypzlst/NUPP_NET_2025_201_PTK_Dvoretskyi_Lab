using Animal.Common;
using System;

namespace Animal.ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            var service = new DogCrudService();

            var dog1 = new Dog("Dog", "Mammal", "Carnivora", "Bobik") { Breed = "Dachshund" };
            var dog2 = new Dog("Dog", "Mammal", "Carnivora", "Sharik") { Breed = "Shepherd" };

            var id1 = service.Create(dog1);
            var id2 = service.Create(dog2);

            Console.WriteLine("\n[READ ALL]");
            foreach (var d in service.ReadAll())
                Console.WriteLine($" - {d.Nickname}, {d.Breed}");

            var newDog = new Dog("Dog", "Mammal", "Carnivora", "Bobik") { Breed = "Corgi" };
            service.Update(id1, newDog);

            service.Delete(id2);

            Console.WriteLine("\n[READ ALL after delete]");
            foreach (var d in service.ReadAll())
                Console.WriteLine($" - {d.Nickname}, {d.Breed}");
        }
    }
}
