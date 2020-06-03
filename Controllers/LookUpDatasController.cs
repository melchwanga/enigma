using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeDatabase.Data;
using EmployeeDatabase.Models;

namespace EmployeeDatabase.Controllers
{
    public class LookUpDatasController : Controller
    {
        private readonly EmployeeDatabaseContext _context;

        public LookUpDatasController(EmployeeDatabaseContext context)
        {
            _context = context;
        }

        // GET: LookUpDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.LookUpData.ToListAsync());
        }

        // GET: LookUpDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lookUpData = await _context.LookUpData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lookUpData == null)
            {
                return NotFound();
            }

            return View(lookUpData);
        }

        // GET: LookUpDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LookUpDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemType,Value")] LookUpData lookUpData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lookUpData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lookUpData);
        }

        // GET: LookUpDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lookUpData = await _context.LookUpData.FindAsync(id);
            if (lookUpData == null)
            {
                return NotFound();
            }
            return View(lookUpData);
        }

        // POST: LookUpDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemType,Value")] LookUpData lookUpData)
        {
            if (id != lookUpData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lookUpData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LookUpDataExists(lookUpData.Id))
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
            return View(lookUpData);
        }

        // GET: LookUpDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lookUpData = await _context.LookUpData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lookUpData == null)
            {
                return NotFound();
            }

            return View(lookUpData);
        }

        // POST: LookUpDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lookUpData = await _context.LookUpData.FindAsync(id);
            _context.LookUpData.Remove(lookUpData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LookUpDataExists(int id)
        {
            return _context.LookUpData.Any(e => e.Id == id);
        }
    }
}
