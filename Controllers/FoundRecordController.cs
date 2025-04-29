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
    public class FoundRecordController : Controller
    {
        private readonly ManagementSystemContext _context;

        public FoundRecordController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: FoundRecord
        public async Task<IActionResult> Index()
        {
            var managementSystemContext = _context.FoundRecords.Include(f => f.Claimant).Include(f => f.Finder).Include(f => f.Item);
            return View(await managementSystemContext.ToListAsync());
        }

        // GET: FoundRecord/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundRecord = await _context.FoundRecords
                .Include(f => f.Claimant)
                .Include(f => f.Finder)
                .Include(f => f.Item)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (foundRecord == null)
            {
                return NotFound();
            }

            return View(foundRecord);
        }

        // GET: FoundRecord/Create
        public IActionResult Create()
        {
            ViewData["ClaimantId"] = new SelectList(_context.Claimants, "ClaimantId", "ClaimantId");
            ViewData["FinderId"] = new SelectList(_context.Finders, "FinderId", "FinderId");
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId");
            return View();
        }

        // POST: FoundRecord/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,ItemId,FinderId,ClaimantId,FoundDate,ClaimDate")] FoundRecord foundRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foundRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClaimantId"] = new SelectList(_context.Claimants, "ClaimantId", "ClaimantId", foundRecord.ClaimantId);
            ViewData["FinderId"] = new SelectList(_context.Finders, "FinderId", "FinderId", foundRecord.FinderId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", foundRecord.ItemId);
            return View(foundRecord);
        }

        // GET: FoundRecord/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundRecord = await _context.FoundRecords.FindAsync(id);
            if (foundRecord == null)
            {
                return NotFound();
            }
            ViewData["ClaimantId"] = new SelectList(_context.Claimants, "ClaimantId", "ClaimantId", foundRecord.ClaimantId);
            ViewData["FinderId"] = new SelectList(_context.Finders, "FinderId", "FinderId", foundRecord.FinderId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", foundRecord.ItemId);
            return View(foundRecord);
        }

        // POST: FoundRecord/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,ItemId,FinderId,ClaimantId,FoundDate,ClaimDate")] FoundRecord foundRecord)
        {
            if (id != foundRecord.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foundRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoundRecordExists(foundRecord.RecordId))
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
            ViewData["ClaimantId"] = new SelectList(_context.Claimants, "ClaimantId", "ClaimantId", foundRecord.ClaimantId);
            ViewData["FinderId"] = new SelectList(_context.Finders, "FinderId", "FinderId", foundRecord.FinderId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId", foundRecord.ItemId);
            return View(foundRecord);
        }

        // GET: FoundRecord/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foundRecord = await _context.FoundRecords
                .Include(f => f.Claimant)
                .Include(f => f.Finder)
                .Include(f => f.Item)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (foundRecord == null)
            {
                return NotFound();
            }

            return View(foundRecord);
        }

        // POST: FoundRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foundRecord = await _context.FoundRecords.FindAsync(id);
            if (foundRecord != null)
            {
                _context.FoundRecords.Remove(foundRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoundRecordExists(int id)
        {
            return _context.FoundRecords.Any(e => e.RecordId == id);
        }
    }
}
