using Fitness.Business.Abstract;
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
    [Authorize(Roles = "Trainer")]
    public class TrainerUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrainerUserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

     
        [HttpGet("my-users")]
        public async Task<IActionResult> GetMyUsers()
        {
            string identityTrainerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainer = await _userManager.FindByIdAsync(identityTrainerId);
          
            if (trainer == null)
                return Unauthorized();

            var users = await _userService.GetUsersByTrainerId(trainer.Id);
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainer = await _userManager.FindByIdAsync(identityUserId);

            var user = await _userService.GetUserById(id);
            if (user == null || user.TrainerId.ToString() != trainer.Id)
                return Forbid();

            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto dto)
        {
            string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainer = await _userManager.FindByIdAsync(identityUserId);

            var user = await _userService.GetUserById(id);
            if (user == null || user.TrainerId.ToString() != trainer.Id)
                return Forbid();

            await _userService.UpdateUser(id, dto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainer = await _userManager.FindByIdAsync(identityUserId);

            var user = await _userService.GetUserById(id);
            if (user == null || user.TrainerId.ToString() != trainer.Id)
                return Forbid();

            await _userService.DeleteUser(id);
            return NoContent();
        }


    }
}
