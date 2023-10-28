using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using F1Pulse.Models;

namespace F1Pulse.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Route( "{controller=Home}/{action=Index}")]

    public IActionResult Index()
    {
        return RedirectToAction("Menu", "News", 
            new {DateTime.Today.Day, DateTime.Today.Month, DateTime.Today.Year,});
    }
    
    [Route("Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }
    
    [Route("Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}