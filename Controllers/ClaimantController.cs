using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAFMS.Models;

namespace LAFMS.Controllers
{
    public class ClaimantController : Controller
    {
        private readonly ManagementSystemContext _context;

        public ClaimantController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Claimant
        public async Task<IActionResult> Index()
        {
            return View(await _context.Claimants.ToListAsync());
        }

        // GET: Claimant/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimant = await _context.Claimants
                .FirstOrDefaultAsync(m => m.ClaimantId == id);
            if (claimant == null)
            {
                return NotFound();
            }

            return View(claimant);
        }

        // GET: Claimant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claimant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimantId,ClaimantName")] Claimant claimant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claimant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claimant);
        }

        // GET: Claimant/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimant = await _context.Claimants.FindAsync(id);
            if (claimant == null)
            {
                return NotFound();
            }
            return View(claimant);
        }

        // POST: Claimant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ClaimantId,ClaimantName")] Claimant claimant)
        {
            if (id != claimant.ClaimantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claimant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimantExists(claimant.ClaimantId))
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
            return View(claimant);
        }

        // GET: Claimant/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimant = await _context.Claimants
                .FirstOrDefaultAsync(m => m.ClaimantId == id);
            if (claimant == null)
            {
                return NotFound();
            }

            return View(claimant);
        }

        // POST: Claimant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var claimant = await _context.Claimants.FindAsync(id);
            if (claimant != null)
            {
                _context.Claimants.Remove(claimant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimantExists(string id)
        {
            return _context.Claimants.Any(e => e.ClaimantId == id);
        }
    }
}
