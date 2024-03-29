using Microsoft.AspNetCore.Identity;

namespace IdeaInvestigator.Data.Models;

public class User : IdentityUser<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}
