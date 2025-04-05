using Fitness.Business.Abstract;
using Fitness.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("take-attendance")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TakeAttendance([FromBody] TakeAttendanceDto dto)
        {
            await _attendanceService.TakeAttendanceAsync(dto);
            return Ok("Attendance saved successfully.");
        }

        [HttpGet("user-attendance/{userId}")]
        public async Task<IActionResult> GetUserAttendance(int userId)
        {
            var result = await _attendanceService.GetAttendanceByUserIdAsync(userId);
            return Ok(result);
        }
    }
}
