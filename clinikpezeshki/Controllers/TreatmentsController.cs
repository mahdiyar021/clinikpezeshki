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
    public class TreatmentsController : Controller
    {

        private readonly MainContext _context;

        public TreatmentsController(MainContext context)
        {
            _context = context;
        }

        [JwtAuthrorize(JwtRoles.doctor)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Treatments.ToListAsync());
        }


        [JwtAuthrorize(JwtRoles.doctor)]
        public IActionResult Create()
        {
           
            return View();
        }

        [JwtAuthrorize(JwtRoles.doctor)]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Id")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treatment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treatment);
        }


        [JwtAuthrorize(JwtRoles.doctor)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatment = await _context.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return NotFound();
            }
            return View(treatment);
        }

        [JwtAuthrorize(JwtRoles.doctor)]
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Name,Id")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treatment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreatmentExists(treatment.Id))
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
            return View(treatment);
        }


        [JwtAuthrorize(JwtRoles.doctor)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatment = await _context.Treatments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treatment == null)
            {
                return NotFound();
            }

            return View(treatment);
        }


        [JwtAuthrorize(JwtRoles.doctor)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treatment = await _context.Treatments.Include(t=>t.Disbursements).FirstOrDefaultAsync(t=>t.Id==id);
            _context.Treatments.Remove(treatment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreatmentExists(int id)
        {
            return _context.Treatments.Any(e => e.Id == id);
        }
    }
}
