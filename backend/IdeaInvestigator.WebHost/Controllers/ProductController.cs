using IdeaInvestigator.BusinessLogic.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdeaInvestigator.BusinessLogic.Models.IM;

namespace IdeaInvestigator.WebHost.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUserService userService;
        private readonly IProductService productService;

        public ProductController(
            IUserService userService,
            IProductService productService)
        {
            this.userService = userService;
            this.productService = productService;
        }

        [HttpPost("create")]
        public IActionResult CreateProduct([FromBody] ProductIM prod)
        {
            var product = productService.CreateProductAsync(prod);
            return Ok(product);
        }

        [HttpGet("get")]
        public IActionResult GetProduct([FromQuery] Guid id)
        {
            var product = productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("get-all")]
        public IActionResult GetAllProducts()
        {
            var products = productService.GetAllProductsAsync();
            return Ok(products);
        }
    }
}