using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolexApplication_BAL.ModelView;
using RolexApplication_BAL.Service.Interface;

namespace RolexApplication_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpPost("/api/v1/Products/CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductVIew product)
        {
            if (product.CategoryId == null)
            {
                return BadRequest("CategoryId is required");
            }
            if (await _categoryService.GetCategoryById(product.CategoryId) == null)
            {
                return BadRequest("Category not found");
            } 
            if(product.Name == null)
            {
                return BadRequest("Name is required");
            }
            if (product.Description == null)
            {
                return BadRequest("Description is required");
            }
            if (product.Price == null)
            {
                return BadRequest("Price is required");
            }
            var checkSuccess = await _productService.AddNewProduct(product);
            if(checkSuccess)
            {
                return Ok("Create successful");
            } else
            {
                return BadRequest("Create fail");
            }
        }
        [HttpGet("/api/v1/Products")]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productService.GetAllProducts();
            if(products != null)
            {
                return Ok(products);
            }
            else
            {
                return BadRequest("Products not avaiable");
            }
        }

        [HttpGet("/api/v1/product/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByID(id);
            if(product != null)
            {
                return Ok(product);
            }
            else
            {
                return BadRequest("Product not found");
            }
        }

        [HttpPut("/api/v1/product")]
        public async Task<IActionResult> UpdateProduct(ProductVIew product)
        {
            if (product.CategoryId == null)
            {
                return BadRequest("CategoryId is required");
            }
            if (await _categoryService.GetCategoryById(product.CategoryId) == null)
            {
                return BadRequest("Category not found");
            }
            if (product.Name == null)
            {
                return BadRequest("Name is required");
            }
            if (product.Description == null)
            {
                return BadRequest("Description is required");
            }
            if (product.Price == null)
            {
                return BadRequest("Price is required");
            }
            var checkSuccess = await _productService.UpdateProduct(product);
            if (checkSuccess)
            {
                return Ok("Update successful");
            }
            else
            {
                return BadRequest("Update fail");
            }
        }

        [HttpPut("/api/v1/product/setStatus/{id}")]
        public async Task<IActionResult> UpdateStatusProduct(int id)
        {
            var check = await _productService.StatusProduct(id);
            if(check)
            {
                return Ok("Update successful");
            }
            else
            {
                return NotFound("Product not found");
            }
        }

    }
}
