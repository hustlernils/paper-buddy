namespace PaperBuddy.Web.Domain;

public class Inproceedings : Publication
{
    public string Publisher { get; set; } = string.Empty;
    public string Booktitle { get; set; } = string.Empty;
    public string Editor { get; set; } = string.Empty;
    public string Volume { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Pages { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Month { get; set; }
}