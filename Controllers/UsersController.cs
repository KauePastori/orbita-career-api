using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orbita.CareerApi.Data;
using Orbita.CareerApi.Models;

namespace Orbita.CareerApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly OrbitaContext _context;

        public UsersController(OrbitaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1" }, user);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, User updated)
        {
            if (id != updated.Id)
                return BadRequest("Id do usuário não confere.");

            var existing = await _context.Users.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = updated.Name;
            existing.Email = updated.Email;
            existing.CurrentRole = updated.CurrentRole;
            existing.WeeklyAvailableHours = updated.WeeklyAvailableHours;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null)
                return NotFound();

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
