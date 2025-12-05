using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public class Chat : TrackedEntity
{
    public ParentType ParentType { get; set; }
    public Guid ParentId { get; set; }
    public Guid UserId { get; set; }
 }