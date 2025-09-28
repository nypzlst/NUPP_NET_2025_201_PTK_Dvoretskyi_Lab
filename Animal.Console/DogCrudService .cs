using System;
using System.Collections.Generic;

namespace Animal.Common
{
    public class DogCrudService
    {
        private readonly Dictionary<Guid, Dog> _dogs = new();

        public Guid Create(Dog dog)
        {
            var id = Guid.NewGuid();
            _dogs[id] = dog;
            Console.WriteLine($"[CREATE] {dog.Nickname} add with Id {id}");
            return id;
        }

        public Dog? Read(Guid id)
        {
            return _dogs.TryGetValue(id, out var dog) ? dog : null;
        }

        public IEnumerable<Dog> ReadAll()
        {
            return _dogs.Values;
        }

        public void Update(Guid id, Dog newDog)
        {
            if (_dogs.ContainsKey(id))
            {
                _dogs[id] = newDog;
                Console.WriteLine($"[UPDATE] Dog with Id {id} update on{newDog.Nickname}");
            }
        }

        public void Delete(Guid id)
        {
            if (_dogs.Remove(id, out var dog))
            {
                Console.WriteLine($"[DELETE] {dog.Nickname} ");
            }
        }
    }
}
