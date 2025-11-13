using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orbita.CareerApi.Data;
using Orbita.CareerApi.Models;

namespace Orbita.CareerApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProgressController : ControllerBase
    {
        private readonly OrbitaContext _context;

        public ProgressController(OrbitaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserMissionProgress>>> GetByUser([FromQuery] Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest("Informe um userId válido.");

            var progresses = await _context.UserMissionProgresses
                .Include(p => p.Mission)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return Ok(progresses);
        }

        [HttpPost]
        public async Task<ActionResult<UserMissionProgress>> Create(UserMissionProgress progress)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = await _context.Users.AnyAsync(u => u.Id == progress.UserId);
            var missionExists = await _context.Missions.AnyAsync(m => m.Id == progress.MissionId);

            if (!userExists || !missionExists)
                return BadRequest("UserId ou MissionId inválido.");

            if (string.IsNullOrWhiteSpace(progress.Status))
                progress.Status = "Pendente";

            _context.UserMissionProgresses.Add(progress);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = progress.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1" }, progress);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserMissionProgress>> GetById(Guid id)
        {
            var progress = await _context.UserMissionProgresses
                .Include(p => p.Mission)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (progress == null)
                return NotFound();

            return Ok(progress);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UserMissionProgress updated)
        {
            if (id != updated.Id)
                return BadRequest("Id do progresso não confere.");

            var existing = await _context.UserMissionProgresses.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Status = updated.Status;
            existing.CompletedAt = updated.CompletedAt;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _context.UserMissionProgresses.FindAsync(id);
            if (existing == null)
                return NotFound();

            _context.UserMissionProgresses.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
