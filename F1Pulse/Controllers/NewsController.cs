using F1Pulse.Models.News;
using F1Pulse.Services.Parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1Pulse.Controllers;
[Route("News/")]
public class NewsController : Controller
{
    private readonly HtmlParser _htmlParser;

    public NewsController(HtmlParser parser)
    {
        _htmlParser = parser;
    }
    
    [HttpGet]
    [Route("")]
    [Route("Menu")]
    [Route("Menu/{day}/{month}/{year}")]
    public IActionResult Menu()
    {
        MenuModel model = new MenuModel();

        _htmlParser.Day = model.Day = Convert.ToInt32(RouteData.Values["day"]);
        _htmlParser.Year = model.Year = Convert.ToInt32(RouteData.Values["year"]);
        _htmlParser.Month = model.Month = Convert.ToInt32(RouteData.Values["month"]);
        model.NewsList = _htmlParser.GetNewsList();
        return View(model);
    }
    
    [HttpPost]
    [Route("Menu/{day}/{month}/{year}")]
    public IActionResult Menu(int day, int month, int year)
    {
        return RedirectToAction("Menu", new {day, month, year});
    }
    
    [HttpGet]
    [Route("Article/{id}")]
    public IActionResult Article()
    {
        ArticleModel model = new ArticleModel();

        model.Id = Convert.ToString(RouteData.Values["id"]);
        model.News = _htmlParser.GetNews(model.Id);
        
        return View(model);
    }
    
}