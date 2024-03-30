using IdeaInvestigator.BusinessLogic.Models.VM;
using IdeaInvestigator.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaInvestigator.BusinessLogic.Models.IM;

namespace IdeaInvestigator.BusinessLogic.Services.Contracts
{
    public interface IProductService
    {
        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>A list with the all the products</returns>
        Task<List<ProductVM>?> GetAllProductsAsync();

        /// <summary>
        /// Gets a product via a supplied product ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>The product's data</returns>
        Task<ProductVM?> GetProductByIdAsync(Guid id);

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="product">Product data</param>
        /// <returns>Product data</returns>
        Task<(ProductVM?, IdentityResult)> CreateProductAsync(ProductIM product);
    }
}
