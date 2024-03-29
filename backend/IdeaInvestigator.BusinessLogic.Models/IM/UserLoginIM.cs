using System.ComponentModel.DataAnnotations;

namespace IdeaInvestigator.BusinessLogic.Models.IM;

public class UserLoginIM
{
    [Required] public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;
}
