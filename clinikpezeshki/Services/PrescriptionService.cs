using clinikpezeshki.Models.Entitys;

namespace clinikpezeshki.Services
{
    public class PrescriptionService : GenericRepository<Prescription>,IPrescriptionService
    {
        public PrescriptionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        public async Task<bool> DeleteWithIdPatient(int idPatient)
        {
            var Prescriptions = GetAll().Where(p => p.PatientId == idPatient).ToList();

            foreach(var Prescription in Prescriptions)
            {
                await DeleteAsync(Prescription);
            }

            return true;
        }
    }
}
