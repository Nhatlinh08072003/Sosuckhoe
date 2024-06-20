using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoSucKhoe.Models;

namespace SoSucKhoe.Controllers;

public class HealthController : Controller
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}