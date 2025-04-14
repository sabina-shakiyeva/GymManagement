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
            return CreatedAtAction(nameof(GetGroupById), new { id = group.Id }, group);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            var result = await _groupService.GetGroupByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("all-groups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var result = await _groupService.GetAllGroupsAsync();
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GroupUpdateDto dto)
        {
            var updated = await _groupService.UpdateGroupAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _groupService.DeleteGroupAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

      
        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserToGroupDto dto)
        {
            var result = await _groupService.AddUserToGroupAsync(dto);
            if (!result) return BadRequest("Qrupa əlavə edilə bilmədi (istifadəçi mövcud olmaya bilər və ya artıq əlavə edilib)");
            return Ok("İstifadəçi qruppa əlavə olundu");
        }
    }
}
