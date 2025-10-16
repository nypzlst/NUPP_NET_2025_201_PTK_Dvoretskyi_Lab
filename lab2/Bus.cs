using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Bus : IEntity
    {
        public Guid Id { get; set; }
        public string? Name{ get; set; }
        public int Capacity { get; set; }
        public double Speed { get; set; }

        private static readonly Random _rand = new();

        public static Bus CreateNew()
        {
            return new Bus
            {
                Id = Guid.NewGuid(),
                Name = $"Bus-{_rand.Next(1000, 9999)}",
                Capacity = _rand.Next(10, 100),
                Speed = Math.Round(_rand.NextDouble() * 120, 2)
            };
        }

    }
}
