namespace UrlShortner.Models;

public class ShortenUrl
{
    public int id { get; set; }
    public string code { get; set; } = string.Empty;
    public string url { get; set; } = string.Empty;
    public DateTime createdOn { get; set; }
}
