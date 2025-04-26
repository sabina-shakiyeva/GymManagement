using Fitness.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartItemController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            await _cartService.AddToCartAsync(userId, productId, quantity);
            return Ok(new { message = "Product added to cart!" });
        }

        [HttpPost("remove-from-cart")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            await _cartService.RemoveFromCartAsync(userId, productId);
            return Ok(new { message = "Product removed from cart!" });
        }
        [HttpPost("update-quantity")]
        public async Task<IActionResult> UpdateQuantity(int userId, int productId, int newQuantity)
        {
            if (newQuantity <= 0)
            {
                
                await _cartService.RemoveFromCartAsync(userId, productId);
                return Ok(new { message = "Product removed from cart because quantity was 0." });
            }

            await _cartService.UpdateQuantityAsync(userId, productId, newQuantity);
            return Ok(new { message = "Product quantity updated!" });
        }
        [HttpGet("get-cart")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cartItems = await _cartService.GetUserCartAsync(userId);
            return Ok(cartItems);
        }

    }
}
