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
    public class MedicinesController : Controller
    {
        private readonly MainContext _context;

        public MedicinesController(MainContext context)
        {
            _context = context;
        }


        [JwtAuthrorize(JwtRoles.doctor)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicines.ToListAsync());
        }

        [JwtAuthrorize(JwtRoles.doctor)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [JwtAuthrorize(JwtRoles.doctor)]
        public async Task<IActionResult> Create([Bind("Name,PersionName,Id")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicine);
        }


        [JwtAuthrorize(JwtRoles.doctor)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return View(medicine);
        }

        [JwtAuthrorize(JwtRoles.doctor)]
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Name,PersionName,Id")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicineExists(medicine.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medicine);
        }


        [JwtAuthrorize(JwtRoles.doctor)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        [JwtAuthrorize(JwtRoles.doctor)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            var medpre =await _context.MedicinePrescriptions.Where(mp => mp.MedicineId == id).ToListAsync();
            _context.MedicinePrescriptions.RemoveRange(medpre);
            await _context.SaveChangesAsync();
            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicineExists(int id)
        {
            return _context.Medicines.Any(e => e.Id == id);
        }
    }
}
