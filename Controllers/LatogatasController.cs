using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MadarfigyeloWeb.Data;
using MadarfigyeloWeb.Models;

namespace MadarfigyeloWeb.Controllers
{
    [Authorize]
    public class LatogatasController : Controller
    {
        private readonly TerepnaploContext _context;

        public LatogatasController(TerepnaploContext context)
        {
            _context = context;
        }

        // GET: Latogatas
        public async Task<IActionResult> Index()
        {
            var terepnaploContext = _context.Latogatas.Include(l => l.Odu);
            return View(await terepnaploContext.ToListAsync());
        }

        // GET: Latogatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latogatas = await _context.Latogatas
                .Include(l => l.Odu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (latogatas == null)
            {
                return NotFound();
            }

            return View(latogatas);
        }

        // GET: Latogatas/Create
        public IActionResult Create()
        {
            ViewData["OduId"] = new SelectList(_context.Odu, "Id", "OduAzonosito");
            return View();
        }

        // POST: Latogatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OduId,Datum,Tevekenyseg,Allapot,Faj,TojasSzam,FiokaSzam,FiokakKora,Megjegyzesek")] Latogatas latogatas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(latogatas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OduId"] = new SelectList(_context.Odu, "Id", "OduAzonosito", latogatas.OduId);
            return View(latogatas);
        }

        // GET: Latogatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latogatas = await _context.Latogatas.FindAsync(id);
            if (latogatas == null)
            {
                return NotFound();
            }
            ViewData["OduId"] = new SelectList(_context.Odu, "Id", "OduAzonosito", latogatas.OduId);
            return View(latogatas);
        }

        // POST: Latogatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OduId,Datum,Tevekenyseg,Allapot,Faj,TojasSzam,FiokaSzam,FiokakKora,Megjegyzesek")] Latogatas latogatas)
        {
            if (id != latogatas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(latogatas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LatogatasExists(latogatas.Id))
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
            ViewData["OduId"] = new SelectList(_context.Odu, "Id", "OduAzonosito", latogatas.OduId);
            return View(latogatas);
        }

        // GET: Latogatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var latogatas = await _context.Latogatas
                .Include(l => l.Odu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (latogatas == null)
            {
                return NotFound();
            }

            return View(latogatas);
        }

        // POST: Latogatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var latogatas = await _context.Latogatas.FindAsync(id);
            if (latogatas != null)
            {
                _context.Latogatas.Remove(latogatas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LatogatasExists(int id)
        {
            return _context.Latogatas.Any(e => e.Id == id);
        }
    }
}
