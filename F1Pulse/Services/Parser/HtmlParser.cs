using HtmlAgilityPack;

namespace F1Pulse.Services.Parser;

public class HtmlParser
{
    public string Year { get; set; } = DateTime.Today.Year.ToString();
    public string Month { get; set; } = DateTime.Today.Month.ToString();
    public string Day { get; set; } = DateTime.Today.Day.ToString();

    

    public List<News> GetNewsList()
    {
        List<News> newsList = new List<News>();
        
        
        String url = $"https://www.f1news.ru/news/{Year}/{Month}/{Day}/";

        HtmlWeb htmlWeb = new HtmlWeb();
        HtmlDocument htmlDoc = htmlWeb.Load(url);

        var newsDivs = htmlDoc.DocumentNode
            .Descendants("div")
            .Where(d => d.Attributes["class"] != null 
                        && d.Attributes["class"].Value.Contains("article article_large"));
        
        foreach (var newsDiv in newsDivs)
        {
            News news = new News();
            
            news.FullContentLink = newsDiv.Descendants("a")
                .First()
                .GetAttributeValue("href", "");
            if (!news.FullContentLink.StartsWith("https://www.f1news.ru"))
            {
                news.FullContentLink = "https://www.f1news.ru" + news.FullContentLink;
            }
            if (news.FullContentLink.EndsWith(".shtml") || !news.FullContentLink.StartsWith("https://www.f1news.ru/news/f1-"))
            {
                continue;
            }
            if (newsList.Count != 0 && news.FullContentLink.Equals(newsList.Last().FullContentLink))
            {
                continue;
            }
            //Console.WriteLine(news.FullContentLink);
            
            news.ImageLink = newsDiv.Descendants("img")
                .First()
                .GetAttributeValue("src", "default");
            if (news.ImageLink.StartsWith("//"))
            {
                news.ImageLink = "https:" + news.ImageLink;
            }
            //Console.WriteLine(news.ImageLink);

            news.Preview = newsDiv.Descendants("div")
                .First(n => n.Attributes["class"] != null 
                            && n.Attributes["class"].Value.Contains("article_content"))
                .InnerText;
            //Console.WriteLine(news.Preview);

            HtmlWeb fullHtmlWeb = new HtmlWeb();
            HtmlDocument fullNews = fullHtmlWeb.Load(news.FullContentLink);

            news.PostDate = fullNews.DocumentNode.Descendants("div")
                .First(n => n.Attributes["class"] != null &&
                             n.Attributes["class"].Value.Equals("post_date"))
                .InnerText;
            //Console.WriteLine(news.PostDate);

            news.Title = fullNews.DocumentNode.Descendants("div")
                .First(n => n.Attributes["class"] != null &&
                            n.Attributes["class"].Value.Equals("post_head"))
                .Element("h1")
                .InnerText;
            //Console.WriteLine(news.Title);

            foreach (var p in fullNews.DocumentNode.Descendants("div")
                         .First(n => n.Attributes["class"] != null &&
                                     n.Attributes["class"].Value.Equals("post_content"))
                         .Elements("p"))
            {
                news.Content.Add(p.InnerText);
            }

            // foreach (var p in news.Content)
            // {
            //     Console.WriteLine(p);                
            // }
            newsList.Add(news);
        }
        return newsList;
    }

    public News GetNews(string id)
    {
        News news = new();
        
        HtmlWeb fullHtmlWeb = new HtmlWeb();
        HtmlDocument fullNews = fullHtmlWeb.Load("https://www.f1news.ru/news/f1-" + id + ".html");
        Console.WriteLine("https://www.f1news.ru/news/f1-" + id + ".html");
        
        news.PostDate = fullNews.DocumentNode.Descendants("div")
            .First(n => n.Attributes["class"] != null &&
                        n.Attributes["class"].Value.Equals("post_date"))
            .InnerText;
        //Console.WriteLine(news.PostDate);

        news.Title = fullNews.DocumentNode.Descendants("div")
            .First(n => n.Attributes["class"] != null &&
                        n.Attributes["class"].Value.Equals("post_head"))
            .Element("h1")
            .InnerText;
        //Console.WriteLine(news.Title);

        foreach (var p in fullNews.DocumentNode.Descendants("div")
                     .First(n => n.Attributes["class"] != null &&
                                 n.Attributes["class"].Value.Equals("post_content"))
                     .Elements("p"))
        {
            news.Content.Add(p.InnerText);
        }
        
        news.ImageLink = fullNews.DocumentNode.Descendants("div")
            .First(n => n.Attributes["class"] != null &&
                             n.Attributes["class"].Value.Equals("post_thumbnail"))
            .Element("img")
            .GetAttributeValue("src", "default");
        if (news.ImageLink.StartsWith("//"))
        {
            news.ImageLink = "https:" + news.ImageLink;
        }

        return news;
    }
}