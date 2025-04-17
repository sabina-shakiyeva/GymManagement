using Fitness.Business.Abstract;
using Fitness.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        //QEYD:swaggerde test etme umumen trainer meselelerinde authorize mutleq qalmalidi cunki giris eden trainere gore userler get ve post oluna bilir postmandan test et

        [HttpGet("statistics")]
        [Authorize(Roles = "Trainer")]
        public async Task<IActionResult> GetStatistics()
        {
            // IdentityTrainerId-ni JWT token-dən çıxarırıq
            var trainerIdentityId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(trainerIdentityId))
                return Unauthorized("Trainer identity not found");

            var stats = await _trainerService.GetTrainerStatisticsAsync(trainerIdentityId);
            return Ok(stats);
        }
        //attendanceleri get methodunu burada yazdim
        [HttpGet("attendance")]
        [Authorize(Roles = "Trainer")]
        public async Task<IActionResult> GetMyUserAttendance()
        {
            var trainerIdentityId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(trainerIdentityId))
                return Unauthorized("Trainer identity not found");

            var attendanceList = await _trainerService.GetTrainerAttendanceListAsync(trainerIdentityId);
            return Ok(attendanceList);
        }
        //buda ele evvel yazdigimiz kimidi take attendance 
        [HttpPost("take-attendance")]
        [Authorize(Roles = "Trainer")]
        public async Task<IActionResult> TakeAttendance([FromBody] TakeAttendanceDto dto)
        {
            var trainerIdentityId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(trainerIdentityId))
                return Unauthorized("Trainer identity not found");

            await _trainerService.TakeAttendanceByTrainerAsync(trainerIdentityId, dto);

            return Ok("Attendance recorded successfully.");
        }



    }
}
