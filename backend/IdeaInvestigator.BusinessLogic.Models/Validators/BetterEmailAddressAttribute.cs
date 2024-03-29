using System.ComponentModel.DataAnnotations;

namespace IdeaInvestigator.BusinessLogic.Models.Validators;

class BetterEmailAddressAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not string email)
            return false;

        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
            return false;
        
        try
        {
            var addr = new System.Net.Mail.MailAddress(trimmedEmail);
            return addr.Address == trimmedEmail || base.IsValid(trimmedEmail);
        }
        catch
        {
            return false;
        }
    }
}