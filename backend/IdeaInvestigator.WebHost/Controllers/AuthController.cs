using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using IdeaInvestigator.BusinessLogic.Services.Contracts;
using IdeaInvestigator.BusinessLogic.Models.IM;

namespace IdeaInvestigator.WebHost.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : Controller
{
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    private readonly IAuthService authService;

    public AuthController(
        IUserService userService,
        ITokenService tokenService,
        IAuthService authService)
    {
        this.userService = userService;
        this.tokenService = tokenService;
        this.authService = authService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserIM userInput)
    {
        if (await authService.CheckUserExistsAsync(userInput.Email))
        {
            return Conflict("user-already-exists");
        }

        var (_, identityResult) = await userService.CreateUserAsync(userInput);

        if (!identityResult.Succeeded)
            return BadRequest(identityResult.Errors.FirstOrDefault()?.Description);
        
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginAsync([FromBody] UserLoginIM userInput)
    {
        if (!await authService.CheckUserExistsAsync(userInput.Email))
        {
            return NotFound("user-not-found");
        }

        if (!await authService.CheckPassword(userInput.Email, userInput.Password))
        {
            return BadRequest("incorrect-password");
        }

        var user = await userService.GetUserByEmailAsync(userInput.Email);

        var token = await tokenService.CreateTokenForUserAsync(user!.Id);

        return Ok($"\"{new JwtSecurityTokenHandler().WriteToken(token)}\"");
    }
}