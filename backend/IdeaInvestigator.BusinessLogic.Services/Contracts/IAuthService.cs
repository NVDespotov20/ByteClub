namespace IdeaInvestigator.BusinessLogic.Services.Contracts;

public interface IAuthService
{
    /// <summary>
    /// Checks if user exists via supplied email
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <returns>A boolean which tells whether the user exists</returns>
    Task<bool> CheckUserExistsAsync(string email);

    /// <summary>
    /// Checks if user's provided password is correct.
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <param name="password">Password of the user</param>
    /// <returns>A boolean which tells whether the password is correct</returns>
    Task<bool> CheckPassword(string email, string password);

    /// <summary>
    /// Tells whether a user can sign in or not provided he has the correct password
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <returns>A boolean which tells whether a user can sign in or not</returns>
    Task<bool> CanUserSignIn(string email);
}
