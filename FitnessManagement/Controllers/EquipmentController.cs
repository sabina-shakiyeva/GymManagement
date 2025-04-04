using Fitness.Business.Abstract;
using FitnessManagement.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEquipment([FromForm] EquipmentDto equipmentDto)
        {
           
            await _equipmentService.AddEquipment(equipmentDto);
            return Ok("Avadanlıq əlavə edildi.");
        }
    }
}
