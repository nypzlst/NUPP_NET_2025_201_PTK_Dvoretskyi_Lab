using lab2;


using lab2.Models;
using Microsoft.AspNetCore.Mvc;
using University.REST.Models;

namespace University.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorsController : ControllerBase
    {
        private readonly ICrudServiceAsync<Professor> _professorService;

        public ProfessorsController(ICrudServiceAsync<Professor> professorService)
        {
            _professorService = professorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessorModel>>> GetAll()
        {
            var professors = await _professorService.ReadAllAsync();

            var models = professors.Select(p => new ProfessorModel
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Department = p.Department,
                HireDate = p.HireDate
            });

            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessorModel>> GetById(Guid id)
        {
            var professor = await _professorService.ReadAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            var model = new ProfessorModel
            {
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                Department = professor.Department,
                HireDate = professor.HireDate
            };

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<ProfessorModel>> Create(Professor professor)
        {
            if (professor.Id == Guid.Empty)
            {
                professor.Id = Guid.NewGuid();
            }

            await _professorService.CreateAsync(professor);

            var model = new ProfessorModel
            {
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                Department = professor.Department,
                HireDate = professor.HireDate
            };

            return CreatedAtAction(nameof(GetById), new { id = professor.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Professor professor)
        {
            if (id != professor.Id)
            {
                return BadRequest();
            }

            var existing = await _professorService.ReadAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _professorService.UpdateAsync(professor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var professor = await _professorService.ReadAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            await _professorService.RemoveAsync(professor);
            return NoContent();
        }

    }
}
