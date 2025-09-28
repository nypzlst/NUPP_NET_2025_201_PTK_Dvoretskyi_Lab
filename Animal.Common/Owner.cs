using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal.Common
{
    public delegate void DogGettingOwner(Owner owner);


    public class Owner
    {
        public string? FullName { get; set; }
        public Guid OwnerId { get; set; }
        public int OwnerAge { get; set; }
        public string? LivingCity { get; set; }

        public event DogGettingOwner? OnDogGettingOwner;

        public void AssignDog(Dog dog)
        {
            Console.WriteLine($"{FullName} owner the {dog.Nickname}!");
            // вызов события
            OnDogGettingOwner?.Invoke(this);
        }
    }
}
