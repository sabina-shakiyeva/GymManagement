using Fitness.Business.Abstract;
using FitnessManagement.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;

        public PaymentController(IPaymentService paymentService, IUserService userService)
        {
            _paymentService = paymentService;
            _userService = userService;


        }

        [HttpPost("buy-package")]
        public async Task<IActionResult> BuyPackage([FromQuery] int userId, [FromQuery] int packageId, [FromBody] PaymentDto paymentDto)
        {
            try
            {
                var result = await _paymentService.PurchasePackageAsync(userId, packageId, paymentDto);
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
