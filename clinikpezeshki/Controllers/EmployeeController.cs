using Microsoft.AspNetCore.Mvc;


namespace clinikpezeshki.Controllers
{
  
    public class EmployeeController : Controller
    {
        [JwtAuthrorize(JwtRoles.employee)]
        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}
