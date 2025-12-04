namespace PaperBuddy.Web.Domain;

public class Book : Publication
{
    public string Publisher { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
}