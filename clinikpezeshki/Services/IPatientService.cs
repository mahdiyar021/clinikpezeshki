using clinikpezeshki.Models.Entitys;
using clinikpezeshki.Models.ViewModels;

namespace clinikpezeshki.Services
{
    public interface IPatientService:IGenericRepository<Patient>
    {
        Task<IEnumerable<PatientVm>> GetPatientsVmAsync();
        Task<Patient?> FindPatientWithAallDetailsAsync(int id);
        Task<bool> DeletePatientWithDependencies(int id);
    }
}