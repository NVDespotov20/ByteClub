using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using IdeaInvestigator.BusinessLogic.Services.Contracts;

namespace IdeaInvestigator.BusinessLogic.Services.Implementations;

public class AuthedUser : IAuthedUser
{
    public AuthedUser(IHttpContextAccessor httpContextAccessor)
    {
        UserId = Guid.Parse(httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);
    }

    public Guid UserId { get; }
}