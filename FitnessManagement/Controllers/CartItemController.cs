using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IUserDal _userDal;

        public CartItemController(ICartService cartService, IUserDal userDal)
        {
            _cartService = cartService;
            _userDal = userDal;

        }
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userIdentityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  

            if (string.IsNullOrEmpty(userIdentityId))
            {
                return Unauthorized("User identity not found");
            }


            var user = await _userDal.Get(u => u.IdentityUserId == userIdentityId);

            if (user == null)
            {
                return Unauthorized("User not found");
            }
            await _cartService.AddToCartAsync(user.Id, productId, quantity);
            return Ok(new { message = "Product added to cart!" });
        }

        [HttpPost("remove-from-cart")]
        public async Task<IActionResult> RemoveFromCart( int productId)
        {
            var userIdentityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  

            if (string.IsNullOrEmpty(userIdentityId))
            {
                return Unauthorized("User identity not found");
            }


            var user = await _userDal.Get(u => u.IdentityUserId == userIdentityId);

            if (user == null)
            {
                return Unauthorized("User not found");
            }
            await _cartService.RemoveFromCartAsync(user.Id, productId);
            return Ok(new { message = "Product removed from cart!" });
        }
        [HttpPost("update-quantity")]
        public async Task<IActionResult> UpdateQuantity( int productId, int newQuantity)
        {
            var userIdentityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 

            if (string.IsNullOrEmpty(userIdentityId))
            {
                return Unauthorized("User identity not found");
            }


            var user = await _userDal.Get(u => u.IdentityUserId == userIdentityId);

            if (user == null)
            {
                return Unauthorized("User not found");
            }
            if (newQuantity <= 0)
            {
                
                await _cartService.RemoveFromCartAsync(user.Id, productId);
                return Ok(new { message = "Product removed from cart because quantity was 0." });
            }

            await _cartService.UpdateQuantityAsync(user.Id, productId, newQuantity);
            return Ok(new { message = "Product quantity updated!" });
        }
        [HttpGet("get-cart")]
        public async Task<IActionResult> GetCart()
        {
            var userIdentityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  

            if (string.IsNullOrEmpty(userIdentityId))
            {
                return Unauthorized("User identity not found");
            }


            var user = await _userDal.Get(u => u.IdentityUserId == userIdentityId);

            if (user == null)
            {
                return Unauthorized("User not found");
            }
            var cartItems = await _cartService.GetUserCartAsync(user.Id);
            return Ok(cartItems);
        }
        [HttpPost("buy-all")]
        public async Task<IActionResult> BuyAll()
        {
         
            var userIdentityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Bu halda, userIdentityId string olacaq

            if (string.IsNullOrEmpty(userIdentityId))
            {
                return Unauthorized("User identity not found");
            }

          
            var user = await _userDal.Get(u => u.IdentityUserId == userIdentityId);

            if (user == null)
            {
                return Unauthorized("User not found");
            }

          
            await _cartService.BuyAllFromCartAsync(user.Id);

            return Ok("Purchase requests created and cart cleared.");
        }


    }
}
