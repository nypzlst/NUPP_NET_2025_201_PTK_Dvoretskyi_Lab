using lab2;
using lab2.Models;
using Microsoft.AspNetCore.Mvc;
using University.REST.Models;

namespace University.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ICrudServiceAsync<Student> _studentService;

        public StudentsController(ICrudServiceAsync<Student> studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentModel>>> GetAll()
        {
            var students = await _studentService.ReadAllAsync();

            var models = students.Select(s => new StudentModel
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                StartOfTraining = s.StartOfTraining,
                EndOfTraining = s.EndOfTraining
            });

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentModel>> GetById(Guid id)
        {
            var student = await _studentService.ReadAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                StartOfTraining = student.StartOfTraining,
                EndOfTraining = student.EndOfTraining
            };

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<StudentModel>> Create(Student student)
        {
            if (student.Id == Guid.Empty)
            {
                student.Id = Guid.NewGuid();
            }

            await _studentService.CreateAsync(student);

            var model = new StudentModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                StartOfTraining = student.StartOfTraining,
                EndOfTraining = student.EndOfTraining
            };

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest("ID in URL and body do not match");
            }

            var existing = await _studentService.ReadAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _studentService.UpdateAsync(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await _studentService.ReadAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            await _studentService.RemoveAsync(student);
            return NoContent();
        }

    }
}
