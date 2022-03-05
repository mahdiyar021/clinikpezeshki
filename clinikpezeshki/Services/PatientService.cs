#nullable disable
using clinikpezeshki.Models.Entitys;
using clinikpezeshki.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace clinikpezeshki.Services
{
    public class PatientService : GenericRepository<Patient>, IPatientService
    {
        private readonly IPrescriptionService _prescriptionService;

        private readonly IDisbursementService _disbursementService;

        public PatientService(IUnitOfWork unitOfWork,
                              IPrescriptionService prescriptionService,
                              IDisbursementService disbursementService) : base(unitOfWork)
        {
            _prescriptionService = prescriptionService;

            _disbursementService = disbursementService;
        }

        public async Task<IEnumerable<PatientVm>> GetPatientsVmAsync()
        {
            return GetAll().Select(p =>
                                 new PatientVm
                                 {
                                     Id = p.Id,
                                     FullName = p.Name + " " + p.LastName,
                                     NationalId = p.NationalId
                                 }).AsEnumerable();
        }

        public async Task<Patient?> FindPatientWithAallDetailsAsync(int id)
        {
            return await GetAll()
                        .Where(p => p.Id == id)
                        .Include(p => p.Disbursements).ThenInclude(D => D.HowPay)
                        .Include(p => p.Disbursements).ThenInclude(D => D.Treatment)
                        .Include(p => p.Prescriptions).ThenInclude(p => p.MedicinePrescriptions).ThenInclude(mp=>mp.Medicine)
                        .FirstOrDefaultAsync();
        }

        public async Task<bool> DeletePatientWithDependencies(int id)
        {
            await _prescriptionService.DeleteWithIdPatient(id);

            await _disbursementService.DeleteWithIdPatient(id);

            return await DeleteAsync(id);

        }

    }
}
