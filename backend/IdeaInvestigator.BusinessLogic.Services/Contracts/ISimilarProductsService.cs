namespace IdeaInvestigator.BusinessLogic.Services.Contracts;

public interface ISimilarProductsService
{
    /// <summary>
    /// Queries the database to find the products of the same category
    /// and sends them to the ML to determine wheater they are similar
    /// to the idea of the user
    /// </summary>
}
