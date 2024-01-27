using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenOpdracht.Data;
using ExamenOpdracht.Models;

namespace ExamenOpdracht.Controllers
{
    public class BestellingsController : Controller
    {
        private readonly ExamenOpdrachtContext _context;

        public BestellingsController(ExamenOpdrachtContext context)
        {
            _context = context;
        }

        // GET: Bestellings
        public async Task<IActionResult> Index()
        {
            var examenOpdrachtContext = _context.Bestelling.Include(b => b.Gebruiker).Include(b => b.Product);
            return View(await examenOpdrachtContext.ToListAsync());
        }

        // GET: Bestellings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling
                .Include(b => b.Gebruiker)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // GET: Bestellings/Create
        public IActionResult Create()
        {
            ViewData["GebruikerId"] = new SelectList(_context.Set<Gebruiker>(), "GebruikerId", "GebruikerNaam");
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Naam");
            return View();
        }

        // POST: Bestellings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BestellingId,BestellingNaam,Datum,Aantal,ProductId,GebruikerId")] Bestelling bestelling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bestelling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GebruikerId"] = new SelectList(_context.Set<Gebruiker>(), "GebruikerId", "GebruikerNaam", bestelling.GebruikerId);
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Naam", bestelling.ProductId);
            return View(bestelling);
        }

        // GET: Bestellings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling.FindAsync(id);
            if (bestelling == null)
            {
                return NotFound();
            }
            ViewData["GebruikerId"] = new SelectList(_context.Set<Gebruiker>(), "GebruikerId", "GebruikerNaam", bestelling.GebruikerId);
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Naam", bestelling.ProductId);
            return View(bestelling);
        }

        // POST: Bestellings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BestellingId,BestellingNaam,Datum,Aantal,ProductId,GebruikerId")] Bestelling bestelling)
        {
            if (id != bestelling.BestellingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestelling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestellingExists(bestelling.BestellingId))
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
            ViewData["GebruikerId"] = new SelectList(_context.Set<Gebruiker>(), "GebruikerId", "GebruikerNaam", bestelling.GebruikerId);
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Naam", bestelling.ProductId);
            return View(bestelling);
        }

        // GET: Bestellings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling
                .Include(b => b.Gebruiker)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // POST: Bestellings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestelling = await _context.Bestelling.FindAsync(id);
            if (bestelling != null)
            {
                _context.Bestelling.Remove(bestelling);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestellingExists(int id)
        {
            return _context.Bestelling.Any(e => e.BestellingId == id);
        }
    }
}
