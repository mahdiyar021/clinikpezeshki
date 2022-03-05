using Microsoft.AspNetCore.Mvc;

namespace clinikpezeshki.Controllers
{
   // [JwtAuthrorize(JwtRoles.doctor)]
    public class DoctorController : Controller
    {
        public async  Task<IActionResult> Index()
        {
            return View();
        }

    }
}
