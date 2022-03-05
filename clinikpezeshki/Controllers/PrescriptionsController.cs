#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinikpezeshki.Contexts;
using clinikpezeshki.Models.Entitys;
using System.Security.Claims;

namespace clinikpezeshki.Controllers;

public class PrescriptionsController : BaseController
{
    private readonly MainContext _context;

    private readonly IJwtServices _jwtServices;

    public PrescriptionsController(MainContext context, IJwtServices jwtServices)
    {
        _context = context;

        _jwtServices = jwtServices;
    }


    [JwtAuthrorize(JwtRoles.doctor)]
    public async Task<IActionResult> Index()
    {

        var IdDoctor = GetUserId();

        var mainContext = _context.Prescriptions
            .Where(p => p.DoctorId == IdDoctor)
            .Include(p => p.Patient);

        return View(await mainContext.ToListAsync());
    }

    private int GetUserId()
    {
        var claims = GetUserClaims();

        var IdClaim = claims[1];

        return Convert.ToInt32(IdClaim.Value);
    }

    [JwtAuthrorize(JwtRoles.doctor)]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prescription = await _context.Prescriptions
            .Include(p => p.Doctor)
            .Include(p => p.Patient)
            .FirstOrDefaultAsync(m => m.Id == id);

        var medpre = await _context.MedicinePrescriptions
                                  .Where(mp => mp.PrescriptionId == id)
                                  .Include(mp => mp.Medicine)
                                  .ToListAsync();

        prescription.MedicinePrescriptions = medpre;
        if (prescription == null)
        {
            return NotFound();
        }

        return View(prescription);
    }


    [JwtAuthrorize(JwtRoles.doctor)]
    public IActionResult Create()
    {
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName");
        ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName");
        ViewData["MedicineList"] = _context.Medicines.ToList();
        return View();
    }

    [JwtAuthrorize(JwtRoles.doctor)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("DateTime,InstroductionForUse,PatientId,DoctorId,Id")] Prescription prescription, List<HasMedicine> hasMedicines)
    {

        if (ModelState.IsValid)
        {
            await _context.AddAsync(prescription);
            await _context.SaveChangesAsync();
            var lp = await _context.Prescriptions.OrderBy(p=>p.Id).LastOrDefaultAsync();
            List<MedicinePrescription> NewMedicinePrescriptions = new();
            for (int i = 0; i < hasMedicines.Count; i++)
            {
                if (hasMedicines[i].IsHasMedicine == true)
                {
                    NewMedicinePrescriptions.Add(new MedicinePrescription()
                    { MedicineId = hasMedicines[i].MedicineId, PrescriptionId = lp.Id});
                }

            }


            return RedirectToAction(nameof(Index));
        }
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName", prescription.DoctorId);
        ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName", prescription.PatientId);
        ViewData["MedicineList"] = _context.Medicines.ToList();
        return View(prescription);

    }


    [JwtAuthrorize(JwtRoles.doctor)]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prescription = await _context.Prescriptions
            .FirstOrDefaultAsync(p => p.Id == id);

        var medpre = await _context.MedicinePrescriptions
                          .Where(mp => mp.PrescriptionId == id)
                          .Include(mp => mp.Medicine)
                          .ToListAsync();

        prescription.MedicinePrescriptions = medpre;
        if (prescription == null)
        {
            return NotFound();
        }
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName", prescription.DoctorId);
        ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName", prescription.PatientId);
        ViewData["MedicineList"] = _context.Medicines.ToList();
        return View(prescription);
    }

    [JwtAuthrorize(JwtRoles.doctor)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("DateTime,InstroductionForUse,PatientId,DoctorId,Id")] Prescription prescription, List<HasMedicine> hasMedicines)
    {

        List<MedicinePrescription> NewMedicinePrescriptions = new();

        for (int i = 0; i < hasMedicines.Count; i++)
        {
            if (hasMedicines[i].IsHasMedicine == true)
            {
                NewMedicinePrescriptions.Add(new MedicinePrescription()
                { MedicineId = hasMedicines[i].MedicineId, PrescriptionId = prescription.Id });
            }

        }
        if (ModelState.IsValid)
        {
            try
            {
                var oldMedicinePrescriptions = _context.MedicinePrescriptions
                       .Where(mp => mp.PrescriptionId == prescription.Id).AsEnumerable();

                _context.RemoveRange(oldMedicinePrescriptions);
                await _context.AddRangeAsync(NewMedicinePrescriptions);
                _context.Update(prescription);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionExists(prescription.Id))
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
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName", prescription.DoctorId);
        ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "FullName", prescription.PatientId);
        ViewData["MedicineList"] = _context.Medicines.ToList();
        return View(prescription);
    }


    [JwtAuthrorize(JwtRoles.doctor)]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prescription = await _context.Prescriptions
            .Include(p => p.Doctor)
            .Include(p => p.Patient)
            .FirstOrDefaultAsync(m => m.Id == id);


        if (prescription == null)
        {
            return NotFound();
        }

        var medpre = await _context.MedicinePrescriptions
                  .Include(mp=>mp.Medicine)
                  .Where(mp => mp.PrescriptionId == id)
                  .ToListAsync();

        prescription.MedicinePrescriptions = medpre;
        return View(prescription);
    }

    [JwtAuthrorize(JwtRoles.doctor)]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var prescription = await _context.Prescriptions
                                         .FirstOrDefaultAsync(p => p.Id == id);

        var medpre = await _context.MedicinePrescriptions
                  .Where(mp => mp.PrescriptionId == id)
                  .ToListAsync();
        _context.MedicinePrescriptions.RemoveRange(medpre);
        _context.Prescriptions.Remove(prescription);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PrescriptionExists(int id)
    {
        return _context.Prescriptions.Any(e => e.Id == id);
    }


    private List<Claim>? GetUserClaims()
    {
        string? SecToken = GetCookieValue(JwtConsts.CookieName);

        var claims = _jwtServices.GetClaims(SecToken);

        return claims as List<Claim>;
    }
}
