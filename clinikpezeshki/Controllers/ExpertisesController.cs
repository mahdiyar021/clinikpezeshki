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
    public class ExpertisesController : Controller
    {
        private readonly MainContext _context;

        public ExpertisesController(MainContext context)
        {
            _context = context;
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Expertise.ToListAsync());
        }
        [JwtAuthrorize(JwtRoles.employee)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Create([Bind("Name,Id")] Expertise expertise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expertise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expertise);
        }
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertise = await _context.Expertise.FindAsync(id);
            if (expertise == null)
            {
                return NotFound();
            }
            return View(expertise);
        }

        [HttpPost]
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Edit( [Bind("Name,Id")] Expertise expertise)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expertise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpertiseExists(expertise.Id))
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
            return View(expertise);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expertise = await _context.Expertise
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expertise == null)
            {
                return NotFound();
            }

            return View(expertise);
        }

        [HttpPost, ActionName("Delete")]
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expertise = await _context.Expertise.Include(e=>e.Doctors).FirstOrDefaultAsync(e=>e.Id==id);
            _context.Expertise.Remove(expertise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpertiseExists(int id)
        {
            return _context.Expertise.Any(e => e.Id == id);
        }
    }
}
