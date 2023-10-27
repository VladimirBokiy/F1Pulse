using System.ComponentModel.DataAnnotations;
using F1Pulse.Data;

namespace F1Pulse.Models.News;

public class ArticleModel
{
    public Services.Parser.News News { get; set; } = new ();
    public List<Comment> Comments { get; set; } = new();
    
    public string Id { get; set; } = string.Empty;
    [Display(Name = "Комментарий...")]
    public string Comment { get; set; } = string.Empty;
}