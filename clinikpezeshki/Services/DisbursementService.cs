using clinikpezeshki.Models.Entitys;

namespace clinikpezeshki.Services
{
    public class DisbursementService : GenericRepository<Disbursement>,IDisbursementService
    {
        public DisbursementService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> DeleteWithIdPatient(int idPatient)
        {
            var Disbursements = GetAll().Where(d => d.PatientId == idPatient).ToList();

            foreach (var Disbursement in Disbursements)
            {
                await DeleteAsync(Disbursement);
            }

            return true;
        }
    }
}
