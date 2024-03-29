using System.ComponentModel.DataAnnotations;
using IdeaInvestigator.BusinessLogic.Models.Validators;

namespace IdeaInvestigator.BusinessLogic.Models.IM;

public class UserIM
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(320, MinimumLength = 1)]
    [BetterEmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [StringLength(42, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
}