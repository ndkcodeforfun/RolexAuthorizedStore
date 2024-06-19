using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolexApplication_BAL.Service.Interface;

namespace RolexApplication_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("/api/v1/categories")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategories();
            if(categories == null)
            {
                return NotFound("Categories not found");
            } 
            return Ok(categories);
        }

    }
}
