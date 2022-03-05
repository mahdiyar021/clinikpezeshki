#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinikpezeshki.Contexts;
using clinikpezeshki.Models.Entitys;
using clinikpezeshki.Models.ViewModels;

namespace clinikpezeshki.Controllers
{
    public class DisbursementsController : Controller
    {
        private readonly MainContext _context;

        public DisbursementsController(MainContext context)
        {
            _context = context;
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Index()
        {
            var D = _context.Disbursements
                .Include(D => D.Patient)
                .Include(D => D.Treatment)
                .Include(D => D.HowPay)
                .AsEnumerable();

            return View(D);
        }


        [JwtAuthrorize(JwtRoles.employee)]
        public IActionResult Create()
        {
            var Vm = new DisbursementsCreateVm()
            {
                Treatments = new SelectList(_context.Treatments.AsEnumerable(),
                                           nameof(Treatment.Id),
                                           nameof(Treatment.Name)),

                HowPays = new SelectList(_context.HowPays.AsEnumerable(),
                                         nameof(HowPay.Id),
                                         nameof(HowPay.Name))
            };

            return View(Vm);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Disbursement disbursement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disbursement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disbursement);
        }

        [HttpGet]
        public async Task<IActionResult> IsPapientExist([FromQuery]Disbursement? disbursement)
        {


            if (disbursement.PatientId == null) return Json(false);

            if(await _context.Patients.AnyAsync(p=>p.Id== disbursement.PatientId))
            {
                var s = HttpContext.Request;
                return Json(true);
            }

            return Json("این بیمار وجود ندارد");
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disbursement = await _context.Disbursements.FindAsync(id);
            if (disbursement == null)
            {
                return NotFound();
            }
            var Vm = new DisbursementsCreateVm()
            {
                Treatments = new SelectList(_context.Treatments.AsEnumerable(),
                               nameof(Treatment.Id),
                               nameof(Treatment.Name),
                               disbursement.TreatmentId),

                HowPays = new SelectList(_context.HowPays.AsEnumerable(),
                             nameof(HowPay.Id),
                             nameof(HowPay.Name),
                             disbursement.HowPayId),

                Disbursement = disbursement
            };

            return View(Vm);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Disbursement disbursement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disbursement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisbursementExists(disbursement.Id))
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
            var Vm = new DisbursementsCreateVm()
            {
                Treatments = new SelectList(_context.Treatments.AsEnumerable(),
                   nameof(Treatment.Id),
                   nameof(Treatment.Name),
                   disbursement.TreatmentId),

                HowPays = new SelectList(_context.HowPays.AsEnumerable(),
                 nameof(HowPay.Id),
                 nameof(HowPay.Name),
                 disbursement.HowPayId),

                Disbursement = disbursement
            };
            return View(Vm);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disbursement = await _context.Disbursements
                                .Include(D => D.Patient)
                .Include(D => D.Treatment)
                .Include(D => D.HowPay)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disbursement == null)
            {
                return NotFound();
            }

            return View(disbursement);
        }

        [JwtAuthrorize(JwtRoles.employee)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disbursement = await _context.Disbursements.FindAsync(id);
            _context.Disbursements.Remove(disbursement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisbursementExists(int id)
        {
            return _context.Disbursements.Any(e => e.Id == id);
        }
    }
}
