using AutoMapper;
using IdeaInvestigator.BusinessLogic.Models.IM;
using IdeaInvestigator.BusinessLogic.Models.VM;
using IdeaInvestigator.BusinessLogic.Services.Contracts;
using IdeaInvestigator.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaInvestigator.BusinessLogic.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<List<ProductVM>> GetAllProductsAsync()
        {
            var products = await context.Products.ToListAsync();

            if (products == null)
                return [];

            return mapper.Map<List<ProductVM>>(products);
        }

        public async Task<ProductVM?> GetProductByIdAsync(Guid id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            return mapper.Map<ProductVM>(product);
        }

        public async Task<ProductVM?> CreateProductAsync(ProductIM product)
        {
            var newProduct = mapper.Map<Product>(product);

            context.Products.Add(newProduct);
            var identityResult = await context.SaveChangesAsync();

            if (identityResult == 0)
                return null;

            return mapper.Map<ProductVM>(newProduct);
        }

        public async Task<List<ProductVM>> GetProductsByCategoryAsync(string category)
        {
            var products = await context.Products.Where(p => p.Category.Contains(category)).ToListAsync();

            if (products == null)
                return [];

            return mapper.Map<List<ProductVM>>(products);
        }

        public async Task<List<ProductVM>> MatchProductsByAtLeastOneCategoryAsync(List<string> categories)
        {
            var products = await context.Products.Where(p => categories.Any(c => p.Category.Contains(c))).ToListAsync();

            if (products == null)
                return [];

            return mapper.Map<List<ProductVM>>(products);
        }
    }
}
