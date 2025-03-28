using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.Entities.Concrete;
using Fitness.Entities.Models;
using FitnessManagement.Dtos;
using FitnessManagement.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FitnessManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
      
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IUserService _userService;
        private readonly ITrainerService _trainerService;
        private readonly IUserDal _userDal;



        public AdminController(UserManager<ApplicationUser> userManager, IUserService userService, IUserDal userDal, ITrainerService trainerService)
        {
            _userManager = userManager;
            _userService = userService;
            _userDal = userDal;
            _trainerService=trainerService;
        }
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser([FromForm] UserDto userDto)
        {
            if(userDto==null)
            {
                return BadRequest(new { Status = "Error", Message = "User data is required!" });
            }
            await _userService.AddUser(userDto);
            return Ok("User added successfully!");

        }
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromForm] UserUpdateDto userUpdateDto)
        {
            try
            {
                await _userService.UpdateUser(userUpdateDto);
                return Ok(new { message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound("Not Found");
            }
            return Ok(user);
        }
      

        [HttpPost("approve-user/{userId}")]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Status = "Error", Message = "User not found!" });
            }
            user.IsApproved = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new { Status = "Error", Message = "Failed to approve the user!" });
            }

            var passwordHash = user.PasswordHash;
            var passwordSalt = user.SecurityStamp;
            var saltBytes = Encoding.UTF8.GetBytes(passwordSalt);
            var hashBytes = Encoding.UTF8.GetBytes(passwordHash);

            var existingUser=await _userDal.Get(u => u.IdentityUserId==user.Id);
            if (existingUser == null)
            {
                var newUser = new User
                {
                    IdentityUserId = user.Id,
                    Name = user.FullName,
                    Email = user.Email,
                    IsActive = true,
                    IsApproved = true,
                    PasswordHash = hashBytes,
                    PasswordSalt= saltBytes,
                    
                };
                await _userDal.Add(newUser);

            }

            return Ok(new { Status = "Success", Message = "User approved successfully!" });
        }

        [HttpGet("pending-users")]
        public async Task<IActionResult> GetPendingUsers()
        {
            var pendingUsers = _userManager.Users.Where(u => !u.IsApproved).Select(u => new
            {
                u.Id,
                u.FullName,
                u.Email,
                u.UserName
            }).ToList();

            return Ok(pendingUsers);
        }
        
        [HttpPost("add-trainer")]
        public async Task<IActionResult> AddTrainer([FromForm] TrainerDto trainerDto)
        {
            if (trainerDto == null)
            {
                return BadRequest(new { Status = "Error", Message = "Trainer data is required!" });
            }
            await _trainerService.AddTrainer(trainerDto);
            return Ok("Trainer added successfully!");

        }

    }
}
