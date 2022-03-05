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
    public class OnlineReservationsController : Controller
    {
        private readonly MainContext _context;

        public OnlineReservationsController(MainContext context)
        {
            _context = context;
        }


        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.OnlineReservations.ToListAsync());
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineReservation = await _context.OnlineReservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (onlineReservation == null)
            {
                return NotFound();
            }

            return View(onlineReservation);
        }

        
        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var onlineReservation = await _context.OnlineReservations.FindAsync(id);
            _context.OnlineReservations.Remove(onlineReservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OnlineReservationExists(int id)
        {
            return _context.OnlineReservations.Any(e => e.Id == id);
        }
    }
}
