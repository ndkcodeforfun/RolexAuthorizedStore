using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RolexApplication_BAL.ModelView;
using RolexApplication_BAL.Service.Interface;
using RolexApplication_DAL.Models;

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

        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetCustomerCartItems(int CustomerId)
        {
            try
            {
                var response = await _cartItemService.GetCartItemsByCustomerId(CustomerId);
                if (!response.Any())
                {
                    return NotFound("No item in your cart");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemInCart(int id)
        {
            try
            {
                var check = await _cartItemService.DeleteItemInCart(id);
                if (check)
                {
                    return Ok("Delete successfully");
                }
                else
                {
                    return BadRequest("Item does not exist");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("api/v1/[controller]/Quantity")]
        public async Task<IActionResult> UpdateItemQuantityInCart([FromQuery] int CartId, [FromQuery] int Quantity)
        {
            try
            {
                var response = await _cartItemService.UpdateItemQuantityInCart(CartId, Quantity);
                if(response == 1)
                {
                    return Ok("Update quantity success");
                }
                else if (response == 3)
                {
                    return Ok("Remove item success");
                }
                else if (response == 2)
                {
                    return BadRequest("Your quantity is greater than number of product in stock");
                } else if (response == -1)
                {
                    return BadRequest("Product is not exist");
                } else if (response == 0)
                {
                    return BadRequest("Item in cart is not exist");
                }
                return BadRequest("Cannot update");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
