namespace F1Pulse.Models.News;

public class MenuModel
{
    public int Year { get; set; } = DateTime.Today.Year;
    public int Month { get; set; } = DateTime.Today.Month;
    public int Day { get; set; } = DateTime.Today.Day;
    public List<Services.Parser.News> NewsList { get; set; } = new List<Services.Parser.News>();
}