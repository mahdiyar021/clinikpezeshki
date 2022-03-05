using clinikpezeshki.Consts.Patient;
using clinikpezeshki.Contexts;
using clinikpezeshki.Models.Entitys;
using clinikpezeshki.Models.ViewModels;
using clinikpezeshki.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinikpezeshki.Controllers;

 
public class PatientController : BaseController
{

    private readonly IPatientService _patientService;


    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [JwtAuthrorize(JwtRoles.employee)]
    public async Task<IActionResult> Index(string? message)
    {
        PatientIndexVm vm = new()
        {
            Message = message,

            PatientsVm = await _patientService.GetPatientsVmAsync()
        };

        return View(vm);
    }

    [JwtAuthrorize(JwtRoles.employee)]
    [HttpGet]
    public async Task<IActionResult> New()
    {

        return View();
    }

    [JwtAuthrorize(JwtRoles.employee)]
    [HttpPost]
    public async Task<IActionResult> New(Patient patient)
    {
        if (ModelState.IsValid)
        {
            await _patientService.AddAsync(patient);

            return RedirectToIndex(PatientMessageText.PatientAdded);
        }


        return RedirectToIndex(PatientMessageText.PatientNotAdded);
    }
    [JwtAuthrorize(JwtRoles.employee)]
    public async Task<IActionResult> Details(int? id)
    {
        if (IsNull(id))
            return RedirectToIndex(PatientMessageText.IdIsnull);

        var patient = await _patientService.FindPatientWithAallDetailsAsync(id ?? 0);

        if (IsNull(patient))
            return RedirectToIndex(PatientMessageText.NotFound);

        return View(patient);
    }
    [JwtAuthrorize(JwtRoles.employee)]
    public async Task<IActionResult> Delete(int? id)
    {
        if (IsNull(id))
            return RedirectToIndex(PatientMessageText.IdIsnull);

        var patient = await _patientService.FindPatientWithAallDetailsAsync(id ?? 0);

        if (IsNull(patient))
            return RedirectToIndex(PatientMessageText.NotFound);

        return View(patient);
    }


    [JwtAuthrorize(JwtRoles.employee)]
    public async Task<IActionResult> DeleteComfirm(int id)
    {
        var IsDeleted = await _patientService.DeletePatientWithDependencies(id);

        if (IsDeleted)
        {
            return RedirectToIndex(PatientMessageText.PatientDeleted);
        }

        return RedirectToIndex(PatientMessageText.PatientNotDeleted);
    }

    [JwtAuthrorize(JwtRoles.employee)]
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)

    {
        if (IsNull(id))
            return RedirectToIndex(PatientMessageText.IdIsnull);

        var patient = await _patientService.FindPatientWithAallDetailsAsync(id ?? 0);

        if (IsNull(patient))
            return RedirectToIndex(PatientMessageText.NotFound);

        return View(patient);
    }

    [JwtAuthrorize(JwtRoles.employee)]
    [HttpPost]
    public async Task<IActionResult> Edit(Patient patient)
    {
        if (ModelState.IsValid)
        {
            _patientService.Update(patient);

            return RedirectToIndex(PatientMessageText.PatientEdited);
        }

        return RedirectToIndex(PatientMessageText.PatientNotEdited);
    }


    private RedirectToActionResult RedirectToIndex(string? message)

     => RedirectToAction(nameof(Index), new { message });

}

