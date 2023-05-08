using F1Pulse.Services.Parser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F1Pulse.Pages;

public class NewsModel : PageModel
{

    private readonly HtmlParser _htmlParser;
    public List<Services.Parser.News> NewsList { get; set; } = new List<Services.Parser.News>();

    [BindProperty(SupportsGet = true)]
    public int Year { get; set; } = DateTime.Today.Year;
    [BindProperty(SupportsGet = true)]
    public int Month { get; set; } = DateTime.Today.Month;
    [BindProperty(SupportsGet = true)]
    public int Day { get; set; } = DateTime.Today.Day;

    public NewsModel()
    {
        _htmlParser = new HtmlParser();
        
        if (Year == 0 && Month == 0 && Day == 0)
        {
            Year = DateTime.Today.Year;
            Month = DateTime.Today.Month;
            Day = DateTime.Today.Day;
        }

        
    }
    public void OnGet()
    {
        ViewData["day"] = Day;
        ViewData["month"] = Month;
        ViewData["year"] = Year;
        
        _htmlParser.Day = Day.ToString();
        _htmlParser.Year = Year.ToString();
        _htmlParser.Month = Month.ToString();
        NewsList = _htmlParser.GetNews();
        Console.WriteLine(NewsList.Count + " items");
    }
}