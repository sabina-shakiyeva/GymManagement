using Fitness.Business.Abstract;
using Fitness.Entities.Models.Group;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupCreateDto dto)
        {
            var group = await _groupService.CreateGroupAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
        }

        // 2. ID ilə qrup gətir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null) return NotFound();
            return Ok(group);
        }

        // 3. Qrupu yenilə
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GroupUpdateDto dto)
        {
            var updated = await _groupService.UpdateGroupAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // 4. Qrupu sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _groupService.DeleteGroupAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // 5. Qrupa user əlavə et
        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserToGroupDto dto)
        {
            var result = await _groupService.AddUserToGroupAsync(dto);
            if (!result) return BadRequest("Qrupa əlavə edilə bilmədi (istifadəçi mövcud olmaya bilər və ya artıq əlavə edilib)");
            return Ok("İstifadəçi qruppa əlavə olundu");
        }
    }
}
