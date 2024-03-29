using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdeaInvestigator.BusinessLogic.Models;
using IdeaInvestigator.BusinessLogic.Services.Contracts;
using IdeaInvestigator.Data.Models;
using IdeaInvestigator.BusinessLogic.Models.IM;
using IdeaInvestigator.BusinessLogic.Models.VM;
using IdeaInvestigator.BusinessLogic.Models.UM;

namespace IdeaInvestigator.BusinessLogic.Services.Implementations;

internal class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public UserService(UserManager<User> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<Tuple<UserVM?, IdentityResult>> CreateUserAsync(UserIM userInput)
    {
        var user = mapper.Map<User>(userInput);
        var identityResult = await userManager.CreateAsync(user, userInput.Password);

        if (!identityResult.Succeeded)
        {
            return new(null, identityResult);
        }

        return new(mapper.Map<UserVM>(user), identityResult);
    }

    public async Task<UserVM?> GetUserByEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        return mapper.Map<UserVM>(user);
    }

    public async Task<UserVM?> GetUserByIdAsync(Guid id)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (user == null)
            return null;

        return mapper.Map<UserVM>(user);
    }

    public async Task<Tuple<UserVM?, IdentityResult?>> UpdateUserAsync(Guid id, UserUM userUpdate)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (user == null)
            return new(null, null);

        user.PhoneNumber = userUpdate.PhoneNumber;
        user.Email = userUpdate.Email;
        user.FirstName = userUpdate.FirstName;
        user.LastName = userUpdate.LastName;
        user.UserName = userUpdate.UserName;

        var identityResult = await userManager.UpdateAsync(user);

        if (identityResult.Succeeded)
            return new(mapper.Map<UserVM>(user), identityResult);
        else return new(null, identityResult);
    }

    public async Task<IdentityResult?> UpdateUserPassword(Guid id, string currentPassword, string newPassword)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (user == null)
            return null;

        return await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }
}