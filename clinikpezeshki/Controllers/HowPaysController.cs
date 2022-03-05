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
    public class HowPaysController : Controller
    {

        private readonly MainContext _context;

        public HowPaysController(MainContext context)
        {
            _context = context;
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.HowPays.ToListAsync());
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Create([Bind("Name,Id")] HowPay howPay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(howPay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(howPay);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var howPay = await _context.HowPays.FindAsync(id);
            if (howPay == null)
            {
                return NotFound();
            }
            return View(howPay);
        }


        [HttpPost]
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Edit([Bind("Name,Id")] HowPay howPay)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(howPay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HowPayExists(howPay.Id))
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
            return View(howPay);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var howPay = await _context.HowPays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (howPay == null)
            {
                return NotFound();
            }

            return View(howPay);
        }


        [HttpPost, ActionName("Delete")]
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var howPay = await _context.HowPays
                                         .Include(h=>h.Disbursements)
                                         .FirstOrDefaultAsync(h=>h.Id==id);

            _context.HowPays.Remove(howPay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HowPayExists(int id)
        {
            return _context.HowPays.Any(e => e.Id == id);
        }
    }
}
