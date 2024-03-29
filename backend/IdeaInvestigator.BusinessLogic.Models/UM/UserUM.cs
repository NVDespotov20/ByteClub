using System.ComponentModel.DataAnnotations;
using IdeaInvestigator.BusinessLogic.Models.Validators;

namespace IdeaInvestigator.BusinessLogic.Models.UM;

public class UserUM
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 4)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [StringLength(320, MinimumLength = 1)]
    [BetterEmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
}
