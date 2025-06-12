using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MadarfigyeloWeb.Data;
using MadarfigyeloWeb.Models;

namespace MadarfigyeloWeb.Controllers
{
    public class DbTestsController : Controller
    {
        private readonly MvcTestContext _context;

        public DbTestsController(MvcTestContext context)
        {
            _context = context;
        }

        // GET: DbTests
        public async Task<IActionResult> Index()
        {
            return View(await _context.Test.ToListAsync());
        }

        // GET: DbTests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbTest = await _context.Test
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbTest == null)
            {
                return NotFound();
            }

            return View(dbTest);
        }

        // GET: DbTests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DbTests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date")] DbTest dbTest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dbTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dbTest);
        }

        // GET: DbTests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbTest = await _context.Test.FindAsync(id);
            if (dbTest == null)
            {
                return NotFound();
            }
            return View(dbTest);
        }

        // POST: DbTests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date")] DbTest dbTest)
        {
            if (id != dbTest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dbTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DbTestExists(dbTest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dbTest);
        }

        // GET: DbTests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbTest = await _context.Test
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dbTest == null)
            {
                return NotFound();
            }

            return View(dbTest);
        }

        // POST: DbTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dbTest = await _context.Test.FindAsync(id);
            if (dbTest != null)
            {
                _context.Test.Remove(dbTest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DbTestExists(int id)
        {
            return _context.Test.Any(e => e.Id == id);
        }

    }
}
