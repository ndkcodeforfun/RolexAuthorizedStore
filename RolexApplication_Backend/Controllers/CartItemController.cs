using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolexApplication_BAL.ModelView;
using RolexApplication_BAL.Service.Interface;

namespace RolexApplication_Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDtoRequest request)
        {
            try
            {
                if (request == null) {
                    return BadRequest("Cannot add empty object to cart");
                }
                await _cartItemService.AddToCart(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
