using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoSucKhoe.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SoSucKhoe.Models.Main;
using SoSucKhoe.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

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
    public IActionResult Phieukhamsuckhoe()
    {
        return View();
    }
    public IActionResult Phieusuckhoedinhki()
    {
        return View();
    }
    public IActionResult Chitietliyychtiem()
    {
        return View();
    }
    public IActionResult Thongketiemchung()
    {
        return View();
    }
    public IActionResult Thongkephieusuckhoe()
    {
        var phieusuckhoe = _sosuckhoeDbContext.PhieuSucKhoes.ToList();
        return View(phieusuckhoe);
    }
    public IActionResult Thongkesuckhoedinhki()
    {
        var phieudinhky = _sosuckhoeDbContext.PhieuDinhKies.ToList();
        return View(phieudinhky);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
