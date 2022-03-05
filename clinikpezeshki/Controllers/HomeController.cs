#nullable disable
using clinikpezeshki.Contexts;
using clinikpezeshki.Models.Entitys;
using clinikpezeshki.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Newtonsoft.Json;
using System.Linq;

namespace clinikpezeshki.Controllers
{
    public class HomeController : BaseController
    {
        private readonly MainContext _context;

        public HomeController(MainContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> AboutUs()
        {

            return View();
        }

        public async Task<IActionResult> Questions()
        {

            return View();
        }
        public async Task<IActionResult> OnlineReserve()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnlineReserve(
        [FromForm]
        [Bind("FullName,Phonenumber,Instroduction")]
        OnlineReservation onlineReservation)
        {
           
            if (ModelState.IsValid)
            {
                await _context.OnlineReservations.AddAsync(onlineReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new MessageboxVm(false, "نوبت شما ذخیره شد"));
            }

            MessageBoxShow(true, "نوبت شما ذخیره نشد");
            return View(onlineReservation);
        }

    }
}
