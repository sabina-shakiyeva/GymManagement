using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using FitnessManagement.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
       
        public PaymentController(IPaymentService paymentService, IUserService userService)
        {
            _paymentService = paymentService;
            _userService = userService;


        }

        //[HttpPost("buy-package")]
        //public async Task<IActionResult> BuyPackage([FromQuery] int userId, [FromQuery] int packageId, [FromBody] PaymentDto paymentDto)
        //{
        //    try
        //    {
        //        var result = await _paymentService.PurchasePackageAsync(userId, packageId, paymentDto);
        //        return Ok(new { message = result });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}
        [HttpPost("buy-package")]
        public async Task<IActionResult> BuyPackage([FromQuery] int packageId, [FromBody] PaymentDto paymentDto)
        {
            try
            {
                var userIdentityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdentityId))
                    return Unauthorized("User identity not found");

                var result = await _paymentService.PurchasePackageAsync(userIdentityId, packageId, paymentDto);
                return Ok(new { message = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("payments")]
        public async Task<IActionResult> GetAllUserPackageTrainer()
        {
            var result = await _userService.GetPayments();
            return Ok(result);
        }
    }
}
