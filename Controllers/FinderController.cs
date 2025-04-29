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
    public class FinderController : Controller
    {
        private readonly ManagementSystemContext _context;

        public FinderController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Finder
        public async Task<IActionResult> Index()
        {
            return View(await _context.Finders.ToListAsync());
        }

        // GET: Finder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finder = await _context.Finders
                .FirstOrDefaultAsync(m => m.FinderId == id);
            if (finder == null)
            {
                return NotFound();
            }

            return View(finder);
        }

        // GET: Finder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Finder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FinderId,FinderName,FinderContact")] Finder finder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(finder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(finder);
        }

        // GET: Finder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finder = await _context.Finders.FindAsync(id);
            if (finder == null)
            {
                return NotFound();
            }
            return View(finder);
        }

        // POST: Finder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FinderId,FinderName,FinderContact")] Finder finder)
        {
            if (id != finder.FinderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(finder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinderExists(finder.FinderId))
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
            return View(finder);
        }

        // GET: Finder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finder = await _context.Finders
                .FirstOrDefaultAsync(m => m.FinderId == id);
            if (finder == null)
            {
                return NotFound();
            }

            return View(finder);
        }

        // POST: Finder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var finder = await _context.Finders.FindAsync(id);
            if (finder != null)
            {
                _context.Finders.Remove(finder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinderExists(string id)
        {
            return _context.Finders.Any(e => e.FinderId == id);
        }
    }
}
