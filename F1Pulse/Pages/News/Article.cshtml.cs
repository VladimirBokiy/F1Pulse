using System.ComponentModel.DataAnnotations;
using F1Pulse.Data;
using F1Pulse.Services.Parser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace F1Pulse.Pages;

public class ArticleModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public InputModel Input { get; set; }
    
    private readonly HtmlParser _htmlParser;
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public Services.Parser.News News { get; set; } = new ();
    public List<Comment> Comments { get; set; } = new();

    public ArticleModel(HtmlParser htmlParser, AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
    {
        _htmlParser = htmlParser;
        _context = appDbContext;
        _userManager = userManager;
    }
    
    public void OnGet()
    {
        News = _htmlParser.GetNews(Input.Id);
        Comments = _context.Comments
            .Include(c => c.ApplicationUser)
            .Where(c => c.NewsId.Equals(Input.Id)).ToList();
    }

    public void OnPost()
    {
        News = _htmlParser.GetNews(Input.Id);
        Comment comment = new Comment();
        comment.Content = Input.Comment;
        comment.CommentDate = DateTime.Now;
        comment.ApplicationUser = _userManager.GetUserAsync(User).Result;
        comment.UserId = _userManager.GetUserAsync(User).Result.Id;
        comment.NewsId = Input.Id;
        _context.Comments.Add(comment);
        _context.SaveChanges();

        Comments = _context.Comments.Where(c => c.NewsId.Equals(Input.Id)).ToList();
    }

    public class InputModel
    {
        public string Id { get; set; } = string.Empty;
        [Display(Name = "Комментарий...")]
        public string Comment { get; set; } = string.Empty;
    }
}