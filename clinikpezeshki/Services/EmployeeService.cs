using clinikpezeshki.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace clinikpezeshki.Services
{
    public class EmployeeService : GenericRepository<Employee>, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Employee?> Find(string username, string password)
        {
            var Result = await GetAll()
                                 .Where(Emp => Emp.Password == password)
                                 .Where(Emp => Emp.Username == username)
                                 .FirstOrDefaultAsync();

            return Result;
        }
    }
}
