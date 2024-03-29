using System.IdentityModel.Tokens.Jwt;

namespace IdeaInvestigator.BusinessLogic.Services.Contracts;

public interface ITokenService
{
    /// <summary>
    /// Creates an access token for a user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>The new access token</returns>
    Task<JwtSecurityToken?> CreateTokenForUserAsync(Guid userId);
}