using F1Pulse.Services.Parser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace F1Pulse.Pages;

[Authorize]
public class ArticleModel : PageModel
{
    [BindProperty(SupportsGet = true)] public string Id { get; set; } = string.Empty;
    public Services.Parser.News News { get; set; } = new ();

    private readonly HtmlParser _htmlParser = new();
    
    public void OnGet()
    {
        News = _htmlParser.GetNews(Id);
    }
}