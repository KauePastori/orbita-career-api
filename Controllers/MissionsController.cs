using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orbita.CareerApi.Data;
using Orbita.CareerApi.Models;

namespace Orbita.CareerApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MissionsController : ControllerBase
    {
        private readonly OrbitaContext _context;

        public MissionsController(OrbitaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetAll([FromQuery] Guid? careerPathId)
        {
            IQueryable<Mission> query = _context.Missions;

            if (careerPathId.HasValue)
            {
                query = query.Where(m => m.CareerPathId == careerPathId.Value);
            }

            var missions = await query.ToListAsync();
            return Ok(missions);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Mission>> GetById(Guid id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
                return NotFound();

            return Ok(mission);
        }

        [HttpPost]
        public async Task<ActionResult<Mission>> Create(Mission mission)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var careerExists = await _context.CareerPaths.AnyAsync(c => c.Id == mission.CareerPathId);
            if (!careerExists)
                return BadRequest("CareerPathId inválido.");

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = mission.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1" }, mission);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, Mission updated)
        {
            if (id != updated.Id)
                return BadRequest("Id da missão não confere.");

            var existing = await _context.Missions.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Title = updated.Title;
            existing.Description = updated.Description;
            existing.Type = updated.Type;
            existing.Difficulty = updated.Difficulty;
            existing.EstimatedMinutes = updated.EstimatedMinutes;
            existing.XpReward = updated.XpReward;
            existing.CareerPathId = updated.CareerPathId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _context.Missions.FindAsync(id);
            if (existing == null)
                return NotFound();

            _context.Missions.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
