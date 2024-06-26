using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoSucKhoe.Models;
using SoSucKhoe.Models.Main;

namespace SoSucKhoe.Controllers;

public class HealthController : Controller
{
    private readonly SosuckhoeDbContext _sosuckhoeDbContext;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IConfiguration _configuration;

    // Constructor
    public HealthController(SosuckhoeDbContext sosuckhoeDbContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _sosuckhoeDbContext = sosuckhoeDbContext;
        _httpContext = httpContextAccessor;
        _configuration = configuration;
    }

    public IActionResult Thongtinsuckhoe()
    {
        return View();
    }
    public IActionResult Suckhoehangngay()
    {
        return View();
    }
    public IActionResult Chiphi()
    {
        return View();
    }
    public IActionResult Theodoisuckhoe()
    {
        return View();
    }
    public IActionResult Lichtiemchung()
    {
        return View();
    }
    public IActionResult Dangkitiemchung()
    {
        return View();
    }
    public IActionResult Thongtinsaukham()
    {
        return View();
    }
    public IActionResult Thongketiemchung()
    {
        return View();
    }
    public IActionResult Thongkephieusuckhoe()
    {
        var PhieuSucKhoes = _sosuckhoeDbContext.PhieuSucKhoes.ToList();
        return View(PhieuSucKhoes);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
