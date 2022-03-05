using clinikpezeshki.Models.Entitys;

namespace clinikpezeshki.Services
{
    public interface IEmployeeService:IGenericRepository<Employee>
    {
        Task<Employee?> Find(string username, string password);
    }
}