using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public class User : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}