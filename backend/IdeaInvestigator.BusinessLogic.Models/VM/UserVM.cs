namespace IdeaInvestigator.BusinessLogic.Models.VM;

public class UserVM
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string UserName { get; set; } = null!;
}