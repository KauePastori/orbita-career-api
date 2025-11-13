using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orbita.CareerApi.Data;
using Orbita.CareerApi.Models;

namespace Orbita.CareerApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CareerPathsController : ControllerBase
    {
        private readonly OrbitaContext _context;

        public CareerPathsController(OrbitaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareerPath>>> GetAll()
        {
            var careerPaths = await _context.CareerPaths.ToListAsync();
            return Ok(careerPaths);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CareerPath>> GetById(Guid id)
        {
            var careerPath = await _context.CareerPaths
                .Include(c => c.Missions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (careerPath == null)
                return NotFound();

            return Ok(careerPath);
        }

        [HttpGet("{id:guid}/missions")]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMissions(Guid id)
        {
            var exists = await _context.CareerPaths.AnyAsync(c => c.Id == id);
            if (!exists)
                return NotFound();

            var missions = await _context.Missions
                .Where(m => m.CareerPathId == id)
                .ToListAsync();

            return Ok(missions);
        }

        [HttpPost]
        public async Task<ActionResult<CareerPath>> Create(CareerPath careerPath)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.CareerPaths.Add(careerPath);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = careerPath.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1" }, careerPath);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, CareerPath updated)
        {
            if (id != updated.Id)
                return BadRequest("Id da rota não confere.");

            var existing = await _context.CareerPaths.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = updated.Name;
            existing.Area = updated.Area;
            existing.Description = updated.Description;
            existing.Level = updated.Level;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _context.CareerPaths.FindAsync(id);
            if (existing == null)
                return NotFound();

            _context.CareerPaths.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
