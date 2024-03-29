using IdeaInvestigator.BusinessLogic.Services.Contracts;
using IdeaInvestigator.BusinessLogic.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace IdeaInvestigator.BusinessLogic.Services;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IAuthedUser, AuthedUser>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ITokenService, TokenService>();
    }
}