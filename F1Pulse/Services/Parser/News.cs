namespace F1Pulse.Services.Parser;

public class News
{
    public String Title { get; set; } = string.Empty;
    public String Preview { get; set; } = string.Empty;
    public List<String> Content { get; set; } = new ();
    public String PostDate { get; set; } = string.Empty;
    public String ImageLink { get; set; } = string.Empty;
    public String FullContentLink { get; set; } = string.Empty;

}