using System.Transactions;
using IdeaInvestigator.BusinessLogic.Models.VM;
using IdeaInvestigator.BusinessLogic.Models.UM;
using IdeaInvestigator.BusinessLogic.Models.IM;
using Microsoft.AspNetCore.Identity;
using IdeaInvestigator.Data.Models;

namespace IdeaInvestigator.BusinessLogic.Services.Contracts;

public interface IUserService
{
    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>A tuple with the newly created user's data and an identity result</returns>
    Task<Tuple<UserVM?, IdentityResult>> CreateUserAsync(UserIM user);
    
    /// <summary>
    /// Gets a user via a supplied user ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>The user's data</returns>
    Task<UserVM?> GetUserByIdAsync(Guid id);

    /// <summary>
    /// Gets a user via a supplied user email
    /// </summary>
    /// <param name="email">Email</param>
    /// <returns>The user's data</returns>
    Task<UserVM?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Updates the user via supplied update data
    /// </summary>
    /// <param name="id">The user's ID</param>
    /// <param name="user">Update data</param>
    /// <returns>A tuple with the updated user's data and an identity result</returns>
    Task<Tuple<UserVM?, IdentityResult?>> UpdateUserAsync(Guid id, UserUM user);

    /// <summary>
    /// Updates the user's password
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="currentPassword">User's current password</param>
    /// <param name="newPassword">New user password</param>
    /// <returns>A boolean which tells whether the user's password has been updated</returns>
    Task<IdentityResult?> UpdateUserPassword(Guid id, string currentPassword, string newPassword);
}