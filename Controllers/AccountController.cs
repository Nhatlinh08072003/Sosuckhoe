using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SoSucKhoe.Models.Main;
using SoSucKhoe.Models;
using SoSucKhoe.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SoSucKhoe.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly SosuckhoeDbContext _context;
    private readonly IConfiguration _configuration;

    public AccountController(ILogger<AccountController> logger, SosuckhoeDbContext context, IConfiguration configuration)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.People
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Pass == model.Password);

            if (user != null && user.Username != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                ViewBag.Username = user.Username;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác. Vui lòng thử lại.");
        }

        return View(model);
    }
    public IActionResult Register()
    {
        return View();
    }
    public IActionResult Forgotpassword()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
