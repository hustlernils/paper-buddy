using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public class ProjectPaper : Entity
{
    public Guid PaperId { get; set; }
    public Guid ProjectId { get; set; }
}