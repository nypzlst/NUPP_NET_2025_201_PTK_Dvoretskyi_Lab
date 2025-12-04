using lab2;
using lab2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.REST.Models;

namespace University.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICrudServiceAsync<Course> _courseService;

        public CoursesController(ICrudServiceAsync<Course> courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CourseModel>>> GetAll()
        {
            var courses = await _courseService.ReadAllAsync();

            var models = courses.Select(c => new CourseModel
            {
                Title = c.Title,
                Credits = c.Credits
            });

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseModel>> GetById(Guid id)
        {
            var course = await _courseService.ReadAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var model = new CourseModel
            {
                Title = course.Title,
                Credits = course.Credits
            };

            return Ok(model);
        }

        [HttpPost]
        [Authorize(Roles = "Librarian,Admin")]
        public async Task<ActionResult<CourseModel>> Create(Course course)
        {
            if (course.Id == Guid.Empty)
            {
                course.Id = Guid.NewGuid();
            }

            await _courseService.CreateAsync(course);

            var model = new CourseModel
            {
                Title = course.Title,
                Credits = course.Credits
            };

            return CreatedAtAction(nameof(GetById), new { id = course.Id }, model);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Librarian,Admin")]
        public async Task<IActionResult> Update(Guid id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            var existing = await _courseService.ReadAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _courseService.UpdateAsync(course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var course = await _courseService.ReadAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            await _courseService.RemoveAsync(course);
            return NoContent();
        }

    }
}
