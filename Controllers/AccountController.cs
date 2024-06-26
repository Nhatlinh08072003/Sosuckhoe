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
    [HttpPost("/account/adduser")]
    public async Task<IActionResult> AddUser(
 [FromForm] string fullname,
 [FromForm] string age,
 [FromForm] string role,
 [FromForm] string username,
 [FromForm] string phonenumber,
 [FromForm] string address,
 [FromForm] string Password,
  [FromForm] string gender)
    {
        if (ModelState.IsValid)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SosuckhoeConnectionString")))
                {
                    await connection.OpenAsync();

                    string sql = @"
                    INSERT INTO Person (Hoten, Username, Phone, AddressUser, Pass, RoleUser,Gender,Age)
VALUES  (@Hoten, @Username, @Phone, @AddressUser, @Pass, @RoleUser ,@Gender,@Age)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Hoten", fullname);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Phone", phonenumber);
                        command.Parameters.AddWithValue("@AddressUser", address);
                        command.Parameters.AddWithValue("@Pass", Password);
                        command.Parameters.AddWithValue("@PhoneNumber", phonenumber);
                        command.Parameters.AddWithValue("@RoleUser", role);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@Age", age);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { success = true, message = "Đăng ký thành công." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false, message = $"Lỗi khi lưu người dùng {ex.Message}" });
            }
        }

        return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
    }
    public IActionResult Forgotpassword()
    {
        return View();
    }
    public IActionResult Thongbao()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
