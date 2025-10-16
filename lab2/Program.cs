using System.Linq;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static async Task Main()
        {
            string filePath = "buses.json";
            var busService = new CrudServiceAsync<Bus>(filePath);

            int total = 1000;

            Parallel.For(0, total, async i =>
            {
                var bus = Bus.CreateNew();
                await busService.CreateAsync(bus);
            });

            await busService.SaveAsync();

            var all = await busService.ReadAllAsync();

            double minSpeed = all.Min(b => b.Speed);
            double maxSpeed = all.Max(b => b.Speed);
            double avgSpeed = all.Average(b => b.Speed);

            int minCapacity = all.Min(b => b.Capacity);
            int maxCapacity = all.Max(b => b.Capacity);
            double avgCapacity = all.Average(b => b.Capacity);
             
            Console.WriteLine($"Speed: Min={minSpeed}, Max={maxSpeed}, Avg={avgSpeed:F2}");
            Console.WriteLine($"Capacity: Min={minCapacity}, Max={maxCapacity}, Avg={avgCapacity:F2}");
            Console.WriteLine($"Saved to: {filePath}");
        }
    }
}