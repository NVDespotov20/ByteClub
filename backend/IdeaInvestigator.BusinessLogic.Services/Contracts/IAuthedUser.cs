namespace IdeaInvestigator.BusinessLogic.Services.Contracts;

public interface IAuthedUser
{
    /// <summary>
    /// The ID of the currently authenticated user
    /// </summary>
    Guid UserId { get; }
}
