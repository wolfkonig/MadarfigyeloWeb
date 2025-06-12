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
    public class OdutelepController : Controller
    {
        private readonly TerepnaploContext _context;

        public OdutelepController(TerepnaploContext context)
        {
            _context = context;
        }

        // GET: Odutelep
        public async Task<IActionResult> Index()
        {
            return View(await _context.Odutelep.ToListAsync());
        }

        // GET: Odutelep/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odutelep = await _context.Odutelep
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odutelep == null)
            {
                return NotFound();
            }

            return View(odutelep);
        }

        // GET: Odutelep/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Odutelep/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Azonosito,Telepules,TeruletNev,UtmNegyzetKod,KezeloSzervezetNev,FelelosSzemelyNev,FelelosSzemelyCim,FelelosSzemelyTelefonszam,FelelosSzemelyEmail,Megjegyzes")] Odutelep odutelep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(odutelep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(odutelep);
        }

        // GET: Odutelep/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odutelep = await _context.Odutelep.FindAsync(id);
            if (odutelep == null)
            {
                return NotFound();
            }
            return View(odutelep);
        }

        // POST: Odutelep/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Azonosito,Telepules,TeruletNev,UtmNegyzetKod,KezeloSzervezetNev,FelelosSzemelyNev,FelelosSzemelyCim,FelelosSzemelyTelefonszam,FelelosSzemelyEmail,Megjegyzes")] Odutelep odutelep)
        {
            if (id != odutelep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odutelep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdutelepExists(odutelep.Id))
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
            return View(odutelep);
        }

        // GET: Odutelep/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odutelep = await _context.Odutelep
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odutelep == null)
            {
                return NotFound();
            }

            return View(odutelep);
        }

        // POST: Odutelep/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odutelep = await _context.Odutelep.FindAsync(id);
            if (odutelep != null)
            {
                _context.Odutelep.Remove(odutelep);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OdutelepExists(int id)
        {
            return _context.Odutelep.Any(e => e.Id == id);
        }
    }
}
