using clinikpezeshki.Consts.Login;
using clinikpezeshki.Models.Entitys;
using clinikpezeshki.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace clinikpezeshki.Controllers;

public class LoginController : BaseController
{
    private readonly IDoctorService _doctorService;

    private readonly IJwtServices _jwtServices;

    private readonly IEmployeeService _employeeService;

    public LoginController(IJwtServices jwtServices,
                           IDoctorService doctorService,
                           IEmployeeService employeeService)
    {
        _doctorService = doctorService;

        _jwtServices = jwtServices;

        _employeeService = employeeService;
    }

    public IActionResult Index(string? Message,[FromQuery]string? RedirectUrl)
    {
        var UserClaim = GetUserClaims();

        if (UserClaim == null)
        {
            return View(new LoginIndexVm() { Message = Message });
        }

        return RedirectToUserController(UserClaim);
    }

    public async Task<IActionResult> LogOut(CancellationToken cancellationToken)

        => await Task.Run(() =>
        {
            HttpContext.Response.Cookies.Delete(JwtConsts.CookieName);
           return RedirectToAction("Index", "Home", new MessageboxVm(true, "شما خارج شدید"));

        }, cancellationToken);
        

        
         
    

    private RedirectToActionResult RedirectToUserController(List<Claim> userClaims)
    {
        foreach (var Claim in userClaims)
        {
            if (IsDoctor(Claim))
            {
                return RedirectToDoctorController();
            }

            if (IsEmployee(Claim))
            {
                return RedirectToEmployeeController();
            }

        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> LoginDoctor([Bind("UserName,Password")] LoginIndexVm loginVm)
    {
        if (ModelState.IsValid)
        {
            var doc = _doctorService.Find(loginVm.UserName, loginVm.Password);

            if (doc == null) return RedirectToIndex(TextMessage.NotFindDoctor);

            var token = CreateToken(doc);

            AppendCookie(JwtConsts.CookieName, token, _jwtServices.GetCookieOptions());

            RedirectToDoctorController();
        }

        return RedirectToIndex(TextMessage.InvalidModel);
    }

    [HttpPost]
    public async Task<IActionResult> LoginEmployee([Bind("UserName,Password")] LoginIndexVm loginVm)
    {
        if (ModelState.IsValid)
        {
            var emp =await _employeeService.Find(loginVm.UserName, loginVm.Password);

            if (emp == null) return RedirectToIndex(TextMessage.NotFindEmployee);

            var token = CreateToken(emp);

            AppendCookie(JwtConsts.CookieName, token, _jwtServices.GetCookieOptions());

            RedirectToDoctorController();
        }

        return RedirectToIndex(TextMessage.InvalidModel);
    }

    private string CreateToken(Doctor doctor)
    {

        IEnumerable<Claim> claims = new List<Claim>
            {
            new Claim(JwtClaimTypes.Role,JwtClaimRoleValue.doctor) ,

            new Claim(JwtClaimTypes.Id,doctor.Id.ToString()),

            new Claim(JwtClaimTypes.Name,doctor.Username),
            };

        return _jwtServices.GetJwtToken(claims);
    }

    private string CreateToken(Employee employee)
    {

        IEnumerable<Claim> claims = new List<Claim>
            {
            new Claim(JwtClaimTypes.Role,JwtClaimRoleValue.employee) ,

            new Claim(JwtClaimTypes.Id,employee.Id.ToString()),

            new Claim(JwtClaimTypes.Name,employee.Username),
            };

        return _jwtServices.GetJwtToken(claims);
    }

    private RedirectToActionResult RedirectToEmployeeController()
    {
        return RedirectToAction("Index", "Employee");
    }

    private RedirectToActionResult RedirectToDoctorController()
    {
        return RedirectToAction("Index", "Doctor");
    }

    private RedirectToActionResult RedirectToIndex(string? Message)
    {
        return RedirectToAction(nameof(Index), new { Message });
    }

    private bool IsDoctor(Claim claim)
    {
        if (claim.Type == JwtClaimTypes.Role &&
              claim.Value == JwtClaimRoleValue.doctor)
        {
            return true;
        }
        return false;
    }

    private bool IsEmployee(Claim claim)
    {
        if (claim.Type == JwtClaimTypes.Role &&
                claim.Value == JwtClaimRoleValue.employee)
        {
            return true;
        }
        return false;
    }

    private List<Claim>? GetUserClaims()
    {
        string? SecToken = GetCookieValue(JwtConsts.CookieName);

        var claims = _jwtServices.GetClaims(SecToken);

        return claims as List<Claim>;
    }
}
