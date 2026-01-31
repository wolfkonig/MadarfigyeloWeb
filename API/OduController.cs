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
    public class OduController : ControllerBase
    {
        private readonly TerepnaploContext _context;

        public OduController(TerepnaploContext context)
        {
            _context = context;
        }

        // GET: api/Odu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Odu>>> GetOdu()
        {
            return await _context.Odu.ToListAsync();
        }

        // GET: api/Odu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Odu>> GetOdu(int id)
        {
            var odu = await _context.Odu.FindAsync(id);

            if (odu == null)
            {
                return NotFound();
            }

            return odu;
        }

        // PUT: api/Odu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOdu(int id, Odu odu)
        {
            if (id != odu.Id)
            {
                return BadRequest();
            }

            _context.Entry(odu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OduExists(id))
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

        // POST: api/Odu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Odu>> PostOdu(Odu odu)
        {
            _context.Odu.Add(odu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOdu", new { id = odu.Id }, odu);
        }

        // DELETE: api/Odu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOdu(int id)
        {
            var odu = await _context.Odu.FindAsync(id);
            if (odu == null)
            {
                return NotFound();
            }

            _context.Odu.Remove(odu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OduExists(int id)
        {
            return _context.Odu.Any(e => e.Id == id);
        }
    }
}
