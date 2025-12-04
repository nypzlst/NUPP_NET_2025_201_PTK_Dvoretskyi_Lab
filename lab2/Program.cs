using lab2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static async Task Main()
        {
            var serviceProvider = new ServiceCollection()
            .AddDbContext<UniversityContext>(options =>
               options.UseSqlite("Data Source=university.db")) 
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceAsync<>))
            .BuildServiceProvider();

 
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<UniversityContext>();    
                await context.Database.EnsureCreatedAsync();
            }

  
            var professorService = serviceProvider.GetRequiredService<ICrudServiceAsync<Professor>>();
            var courseService = serviceProvider.GetRequiredService<ICrudServiceAsync<Course>>();

  
            var professor = new Professor
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Department = "Computer Science",
                HireDate = DateTime.Now.AddYears(-5)
            };
            await professorService.CreateAsync(professor);
            Console.WriteLine($"Created Professor: {professor.LastName}");

            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Entity Framework Core Basics",
                Credits = 5,
                ProfessorId = professor.Id
            };
            await courseService.CreateAsync(course);
            Console.WriteLine($"Created Course: {course.Title} linked to {professor.LastName}");

            var allProfessors = await professorService.ReadAllAsync();
            foreach (var p in allProfessors)
            {
                Console.WriteLine($"Found in DB: {p.FirstName} {p.LastName}, Dept: {p.Department}");
            }
        }
    }
}