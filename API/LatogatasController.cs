using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MadarfigyeloWeb.Data;
using MadarfigyeloWeb.Models;

namespace MadarfigyeloWeb.API
{
    [Route("api/[controller]")]
    [ApiController]
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
            return await _context.Latogatas.ToListAsync();
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
