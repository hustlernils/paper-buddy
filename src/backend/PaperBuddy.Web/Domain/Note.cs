namespace PaperBuddy.Web.Domain;

public class Note
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ParentType ParentType { get; set; }
    public Guid ParentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}