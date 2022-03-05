using clinikpezeshki.Models.Entitys;

namespace clinikpezeshki.Services
{
    public interface IPrescriptionService:IGenericRepository<Prescription>
    {
        Task<bool> DeleteWithIdPatient(int idPatient);
    }
}
