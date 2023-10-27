using F1Pulse.Models.News;
using F1Pulse.Services.Parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1Pulse.Controllers;
[Route("News/")]
public class NewsController : Controller
{
    private readonly HtmlParser _htmlParser;

    public NewsController()
    {
        _htmlParser = new HtmlParser();
    }
    
    [HttpGet]
    [Route("Menu/{day}/{month}/{year}")]
    public IActionResult Menu(int day, int month, int year)
    {
        MenuModel model = new MenuModel();

        _htmlParser.Day = model.Day = Convert.ToInt32(RouteData.Values["day"]);
        _htmlParser.Year = model.Year = Convert.ToInt32(RouteData.Values["month"]);
        _htmlParser.Month = model.Month = Convert.ToInt32(RouteData.Values["year"]);
        Console.WriteLine(RouteData.Values["day"]);
        Console.WriteLine(_htmlParser.Day);
        Console.WriteLine(model.Day);
        model.NewsList = _htmlParser.GetNewsList();
        Console.WriteLine("News found: " + model.NewsList.Count);
        
        return View(model);
    }
    
}