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
        /// Gets all ideas from history
        /// </summary>
        /// <returns>A list with the all the ideas</returns>
        Task<List<IdeaVM>?> GetAllIdeasAsync();

        /// <summary>
        /// Gets a product via a supplied idea ID
        /// </summary>
        /// <param name="id">Idea ID</param>
        /// <returns>The idea's data</returns>
        Task<IdeaVM?> GetIdeaByIdAsync(Guid id);
    }
}
