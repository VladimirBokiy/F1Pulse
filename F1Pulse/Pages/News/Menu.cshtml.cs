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
    public InputModel Input { get; set; } = new InputModel();

    public NewsModel()
    {
        _htmlParser = new HtmlParser();
    }
    public void OnGet()
    {
        _htmlParser.Day = Input.Day.ToString();
        _htmlParser.Year = Input.Year.ToString();
        _htmlParser.Month = Input.Month.ToString();
        NewsList = _htmlParser.GetNewsList();
    }

    public IActionResult OnPost()
    {
        Console.WriteLine(Input.Day + " " + Input.Month + " " + Input.Year);

        return RedirectToPage("/News/Menu", new { year = Input.Year, month = Input.Month, day = Input.Day });
    }

    public class InputModel
    {
        public int Year { get; set; } = DateTime.Today.Year;
        public int Month { get; set; } = DateTime.Today.Month;
        public int Day { get; set; } = DateTime.Today.Day;
    }
}