#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinikpezeshki.Contexts;
using clinikpezeshki.Models.Entitys;

namespace clinikpezeshki.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MainContext _context;

        public EmployeesController(MainContext context)
        {
            _context = context;
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Create([Bind("Name,LastName,Username,Password,Age,Gender,NationalId,HomeNumber,Phonenumber,Adrress,Id")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Edit([Bind("Name,LastName,Username,Password,Age,Gender,NationalId,HomeNumber,Phonenumber,Adrress,Id")] Employee employee)
        {

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
            return View(employee);
        }


        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
