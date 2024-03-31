using IdeaInvestigator.BusinessLogic.Models.IM;
using IdeaInvestigator.BusinessLogic.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaInvestigator.BusinessLogic.Services.Contracts
{
    public interface IIdeaService
    {
        /// <summary>
        /// Gets all ideas
        /// </summary>
        /// <returns>A list with the all the ideas</returns>
        Task<List<IdeaVM>?> GetAllIdeasAsync();

        /// <summary>
        /// Gets a product via a supplied idea ID
        /// </summary>
        /// <param name="id">Idea ID</param>
        /// <returns>The idea's data</returns>
        Task<IdeaVM?> GetIdeaByIdAsync(Guid id);

        /// <summary>
        /// Gets all the idea topics created by the user id
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <returns></returns>
        Task<List<string>?> GetAllIdeaTopicsByUserAsync(Guid userId);


        /// <summary>
        /// Creates a new idea with the user id
        /// </summary>
        /// <param name="idea">the idea</param>
        /// <param name="userId">the id of the user</param>
        /// <returns></returns>
        Task<IdeaVM?> CreateNewIdeaAsync(IdeaIM idea, Guid userId);

        /// <summary>
        /// Gets the idea categories
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string?> GetIdeaCategoriesAsync(Guid id);
    }
}
