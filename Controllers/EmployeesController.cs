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
    public class EmployeesController : Controller
    {
        private readonly EmployeeDatabaseContext _context;

        public EmployeesController(EmployeeDatabaseContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employeeDatabaseContext = _context.Employee.Include(e => e.Gender).Include(e => e.MaritalStatus).Include(e => e.SubCounty).Include(e => e.SubCounty.County);
            return View(await employeeDatabaseContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Gender)
                .Include(e => e.MaritalStatus)
                .Include(e => e.SubCounty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var LookUpData = _context.LookUpData;
            var MaritalStatus = LookUpData.Where(b => b.ItemType == ItemType.MaritalStatus).ToList();
            var Gender = LookUpData.Where(b => b.ItemType == ItemType.Gender).ToList();

            ViewData["GenderId"] = new SelectList(Gender, "Id", "Value");
            ViewData["MaritalStatusId"] = new SelectList(MaritalStatus, "Id", "Value");
            ViewData["CountyID"] = new SelectList(_context.County, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,EmailAddress,PhoneNumber,DOB,GenderId,MaritalStatusId,SubCountyID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var LookUpData = _context.LookUpData;
            var MaritalStatus = LookUpData.Where(b => b.ItemType == ItemType.MaritalStatus).ToList();
            var Gender = LookUpData.Where(b => b.ItemType == ItemType.Gender).ToList();

            ViewData["GenderId"] = new SelectList(Gender, "Id", "Value", employee.GenderId);
            ViewData["MaritalStatusId"] = new SelectList(MaritalStatus, "Id", "Value", employee.MaritalStatusId);
            ViewData["CountyID"] = new SelectList(_context.County, "Id", "Name", employee.SubCounty.County.Id );
            ViewData["SubCountyID"] = new SelectList(_context.SubCounty, "Id", "Name", employee.SubCountyID);

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var LookUpData = _context.LookUpData;
            var MaritalStatus = LookUpData.Where(b => b.ItemType == ItemType.MaritalStatus).ToList();
            var Gender = LookUpData.Where(b => b.ItemType == ItemType.Gender).ToList();

            var SubCounties = _context.SubCounty;//.Where(b => b.CountyId == employee.SubCounty.CountyId).ToList();

            ViewData["GenderId"] = new SelectList(Gender, "Id", "Value", employee.GenderId);
            ViewData["MaritalStatusId"] = new SelectList(MaritalStatus, "Id", "Value", employee.MaritalStatusId);

            ViewData["CountyID"] = new SelectList(_context.County, "Id", "Name");
            ViewData["SubCountyID"] = new SelectList(SubCounties, "Id", "Name");

            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,EmailAddress,PhoneNumber,DOB,GenderId,MaritalStatusId,SubCountyID")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            var LookUpData = _context.LookUpData;
            var MaritalStatus = LookUpData.Where(b => b.ItemType == ItemType.MaritalStatus).ToList();
            var Gender = LookUpData.Where(b => b.ItemType == ItemType.Gender).ToList();

            ViewData["GenderId"] = new SelectList(Gender, "Id", "Value", employee.GenderId);
            ViewData["MaritalStatusId"] = new SelectList(MaritalStatus, "Id", "Value", employee.MaritalStatusId);
            ViewData["CountyID"] = new SelectList(_context.County, "Id", "Name", employee.SubCounty.County.Id);
            ViewData["SubCountyID"] = new SelectList(_context.SubCounty, "Id", "Name", employee.SubCountyID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Gender)
                .Include(e => e.MaritalStatus)
                .Include(e => e.SubCounty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
