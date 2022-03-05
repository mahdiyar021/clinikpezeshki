﻿#nullable disable
using clinikpezeshki.Contexts;
using clinikpezeshki.Models.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace clinikpezeshki.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly MainContext _context;

        public DoctorsController(MainContext context)
        {
            _context = context;
        }
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Doctors.Include(D => D.Expertise).ToListAsync());
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .Include(D => D.Expertise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public IActionResult Create()
        {
            DoctorsCreateVm vm = new()
            {
                Expertises = new SelectList(_context.Expertise.AsEnumerable(),
                                                              nameof(Expertise.Id),
                                                              nameof(Expertise.Name))
            };

            return View(vm);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {

            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            DoctorsCreateVm vm = new()
            {
                Expertises = new SelectList(_context.Expertise.AsEnumerable()
                                                              ,nameof(Expertise.Id)
                                                              ,nameof(Expertise.Name)),
                Doctor= doctor
            };
            return View(vm);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,LastName,Username,Password,Age,Gender,NationalId,HomeNumber,Phonenumber,Adrress,CodeDoctor,Id")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
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
            return View(doctor);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctors
                .Include(d=>d.Prescriptions).FirstOrDefaultAsync(d=>d.Id== id);

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}
