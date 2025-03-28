using Fitness.Entities.Concrete;
using FitnessManagement.Dtos;
using FitnessManagement.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("signup-admin")]
        public async Task<IActionResult> SignUpAdmin([FromBody] RegisterModel dto)
        {
            var admin = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                IsApproved = true 
            };

            var result = await _userManager.CreateAsync(admin, dto.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                await _userManager.AddToRoleAsync(admin, "Admin");
                return Ok(new { Status = "Success", Message = "Admin created successfully!" });
            }

            return BadRequest(new { Status = "Error", Message = "Admin creation failed!", Errors = result.Errors });
        }



        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterModel dto)
        {
            //if (string.IsNullOrEmpty(dto.Role))
            //{
            //    return BadRequest(new { Status = "Error", Message = "Role is required!" });
            //}
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,

                IsApproved = false 
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                    //await _roleManager.CreateAsync(new IdentityRole(dto.Role));
                }

                await _userManager.AddToRoleAsync(user, "User");

                return Ok(new { Status = "Success", Message = $"User registered successfully! Waiting for admin approval." });
            }

            return BadRequest(new { Status = "Error", Message = "Registration failed!", Errors = result.Errors });
        }



     
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginModel dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return Unauthorized(new { Status = "Error", Message = "Invalid username or password!" });
            }

            if (!user.IsApproved)
            {
                return Unauthorized(new { Status = "Error", Message = "Your account is not approved yet. Please wait for admin approval." });
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = GetToken(authClaims);

            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = token.ValidTo });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}



   