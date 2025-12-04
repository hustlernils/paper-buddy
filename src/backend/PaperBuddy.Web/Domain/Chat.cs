namespace PaperBuddy.Web.Domain;

public class Chat
{
    public Guid Id { get; set; }
    public ParentType ParentType { get; set; }
    public Guid ParentId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}