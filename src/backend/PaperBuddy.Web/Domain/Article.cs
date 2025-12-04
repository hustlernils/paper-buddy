namespace PaperBuddy.Web.Domain;

public class Article : Publication
{
    public string Journal { get; set; } = string.Empty;
    public string Volume { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Pages { get; set; } = string.Empty;
    public string Issn { get; set; } = string.Empty;
}