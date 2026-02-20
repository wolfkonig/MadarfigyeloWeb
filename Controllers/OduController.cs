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
    public class OduController : Controller
    {
        private readonly TerepnaploContext _context;

        public OduController(TerepnaploContext context)
        {
            _context = context;
        }

        // GET: Odu
        public async Task<IActionResult> Index()
        {
            var terepnaploContext = _context.Odu.Include(o => o.Odutelep);
            return View(await terepnaploContext.ToListAsync());
        }

        // GET: Odu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odu = await _context.Odu
                .Include(o => o.Odutelep)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odu == null)
            {
                return NotFound();
            }

            return View(odu);
        }

        // GET: Odu/Create
        public IActionResult Create()
        {

            //var items = _context.Odutelep
            //    .Select(x => new SelectListItem(x.Azonosito, x.Id.ToString()));

            ViewData["OdutelepId"] = new SelectList(_context.Odutelep, "Id", "Azonosito");
            return View();
        }

        // POST: Odu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OduAzonosito,OdutelepId,OduTipus,BejaratiNyilasMm,GpsLatitude,GpsLongitude,Elohelykod,MireVanHelyezve,FelhelyezesModja,OduTajolasa,OdutTartoNovenyfaj,MagassagMeter")] Odu odu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(odu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OdutelepId"] = new SelectList(_context.Odutelep, "Id", "Azonosito", odu.OdutelepId);
            return View(odu);
        }

        // GET: Odu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odu = await _context.Odu.FindAsync(id);
            if (odu == null)
            {
                return NotFound();
            }
            ViewData["OdutelepId"] = new SelectList(_context.Odutelep, "Id", "Azonosito", odu.OdutelepId);
            return View(odu);
        }

        // POST: Odu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OduAzonosito,OdutelepId,OduTipus,BejaratiNyilasMm,GpsLatitude,GpsLongitude,Elohelykod,MireVanHelyezve,FelhelyezesModja,OduTajolasa,OdutTartoNovenyfaj,MagassagMeter")] Odu odu)
        {
            if (id != odu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OduExists(odu.Id))
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
            ViewData["OdutelepId"] = new SelectList(_context.Odutelep, "Id", "Azonosito", odu.OdutelepId);
            return View(odu);
        }

        // GET: Odu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odu = await _context.Odu
                .Include(o => o.Odutelep)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odu == null)
            {
                return NotFound();
            }

            return View(odu);
        }

        // POST: Odu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odu = await _context.Odu.FindAsync(id);
            if (odu != null)
            {
                _context.Odu.Remove(odu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OduExists(int id)
        {
            return _context.Odu.Any(e => e.Id == id);
        }
    }
}
