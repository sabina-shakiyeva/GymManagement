using Fitness.Business.Abstract;
using Fitness.Business.Concrete;
using Fitness.Entities.Concrete;
using Fitness.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet("top-users")]
        public async Task<IActionResult> GetTopUsers()
        {
            var topUsers = await _userService.GetTop10UsersByPointsAsync();
            return Ok(topUsers);
        }
        //asagida yazdigim iki endpointde user-in cedvelinde package ve traineri gormesi ucun yazilib
        [HttpGet("details")]
        public async Task<IActionResult> GetAllUserPackageTrainer()
        {
            var result = await _userService.GetAllUserPackageTrainer();
            return Ok(result);
        }
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetUserPackageTrainer(int id)
        {
            try
            {
                var result = await _userService.GetUserPackageTrainer(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("{id}/details")]
        public async Task<IActionResult> UpdateUserPackageTrainer(int id, [FromForm] UserPackageTrainerUpdateDto dto)
        {
            try
            {
                await _userService.UpdateUserPackageTrainer(id, dto);
                return Ok(new { message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

     
     

    }
}
