using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MadarfigyeloWeb.Data;
using MadarfigyeloWeb.Models;

namespace MadarfigyeloWeb.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OdutelepController : ControllerBase
    {
        private readonly TerepnaploContext _context;

        public OdutelepController(TerepnaploContext context)
        {
            _context = context;
        }

        // GET: api/Odutelep
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Odutelep>>> GetOdutelep()
        {
            return await _context.Odutelep.ToListAsync();
        }

        // GET: api/Odutelep/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Odutelep>> GetOdutelep(int id)
        {
            var odutelep = await _context.Odutelep.FindAsync(id);

            if (odutelep == null)
            {
                return NotFound();
            }

            return odutelep;
        }

        // PUT: api/Odutelep/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOdutelep(int id, Odutelep odutelep)
        {
            if (id != odutelep.Id)
            {
                return BadRequest();
            }

            _context.Entry(odutelep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OdutelepExists(id))
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

        // POST: api/Odutelep
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Odutelep>> PostOdutelep(Odutelep odutelep)
        {
            _context.Odutelep.Add(odutelep);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOdutelep", new { id = odutelep.Id }, odutelep);
        }

        // DELETE: api/Odutelep/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOdutelep(int id)
        {
            var odutelep = await _context.Odutelep.FindAsync(id);
            if (odutelep == null)
            {
                return NotFound();
            }

            _context.Odutelep.Remove(odutelep);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OdutelepExists(int id)
        {
            return _context.Odutelep.Any(e => e.Id == id);
        }
    }
}
