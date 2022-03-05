using clinikpezeshki.Models.Entitys;

namespace clinikpezeshki.Services
{
    public interface IDoctorService: IGenericRepository<Doctor>
    {
        public Doctor? Find(string username, string password);
    }
}
