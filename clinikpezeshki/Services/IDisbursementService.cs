using clinikpezeshki.Models.Entitys;

namespace clinikpezeshki.Services
{
    public interface IDisbursementService:IGenericRepository<Disbursement>
    {
        Task<bool> DeleteWithIdPatient(int idPatient);
    }
}