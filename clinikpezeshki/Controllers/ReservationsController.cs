#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinikpezeshki.Contexts;
using clinikpezeshki.Models.Entitys;

namespace clinikpezeshki.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly MainContext _context;

        public ReservationsController(MainContext context)
        {
            _context = context;
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Index()
        {
            var mainContext = _context.Reservations.Include(r => r.Doctor).Include(r => r.Patient);
            return View(await mainContext.ToListAsync());
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, nameof(Doctor.Id), nameof(Doctor.FullName));
            ViewData["PatientId"] = new SelectList(_context.Patients, nameof(Patient.Id), nameof(Patient.FullName));
            return View();
        }

        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,PatientId,Time,date,Id")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, nameof(Doctor.Id), nameof(Doctor.FullName), reservation.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, nameof(Patient.Id), nameof(Patient.FullName), reservation.PatientId);
            return View(reservation);
        }


        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, nameof(Doctor.Id), nameof(Doctor.FullName), reservation.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, nameof(Patient.Id), nameof(Patient.FullName), reservation.PatientId);
            return View(reservation);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId,PatientId,Time,date,Id")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctors, nameof(Doctor.Id), nameof(Doctor.FullName), reservation.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, nameof(Patient.Id), nameof(Patient.FullName), reservation.PatientId);
            return View(reservation);
        }


        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Doctor)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
