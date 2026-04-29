using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MadarfigyeloWeb.Data;
using MadarfigyeloWeb.Models;

namespace MadarfigyeloWeb.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class LatogatasController : ControllerBase
    {
        private readonly TerepnaploContext _context;

        public LatogatasController(TerepnaploContext context)
        {
            _context = context;
        }

        // GET: api/Latogatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Latogatas>>> GetLatogatas()
        {
            return await _context.Latogatas
                .Include(l => l.Odu)
                .OrderBy(l => l.Datum)
                .ToListAsync();
        }

        // GET: api/Latogatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Latogatas>> GetLatogatas(int id)
        {
            var latogatas = await _context.Latogatas.FindAsync(id);

            if (latogatas == null)
            {
                return NotFound();
            }

            return latogatas;
        }

        // GET: api/Latogatas/ByParent/5
        [HttpGet("ByParent/{oduId}")]
        public async Task<ActionResult<IEnumerable<Latogatas>>> GetLatogatasByOdu(int oduId)
        {
            return await _context.Latogatas
                .Where(l => l.OduId == oduId)
                .Include(l => l.Odu)
                .OrderBy(l => l.Datum)
                .ToListAsync();
        }

        // PUT: api/Latogatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLatogatas(int id, Latogatas latogatas)
        {
            if (id != latogatas.Id)
            {
                return BadRequest();
            }

            _context.Entry(latogatas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LatogatasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Latogatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Latogatas>> PostLatogatas(Latogatas latogatas)
        {
            _context.Latogatas.Add(latogatas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLatogatas", new { id = latogatas.Id }, latogatas);
        }

        // DELETE: api/Latogatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLatogatas(int id)
        {
            var latogatas = await _context.Latogatas.FindAsync(id);
            if (latogatas == null)
            {
                return NotFound();
            }

            _context.Latogatas.Remove(latogatas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LatogatasExists(int id)
        {
            return _context.Latogatas.Any(e => e.Id == id);
        }
    }
}
