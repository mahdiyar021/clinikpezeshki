using clinikpezeshki.Models.Entitys;
namespace clinikpezeshki.Services
{
    public class DoctorService : GenericRepository<Doctor>, IDoctorService
    {
        public DoctorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Doctor? Find(string username, string password)
        {
            var Result = GetAll().Where(doc => doc.Password == password)
                                 .Where(doc => doc.Username == username)
                                 .FirstOrDefault();

            return Result;
        }

    }
}
