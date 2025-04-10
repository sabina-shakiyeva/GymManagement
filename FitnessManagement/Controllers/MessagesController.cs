using Fitness.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory(int userId, int receiverId)
        {
            var messages = await _messageService.GetChatHistoryAsync(userId, receiverId);
            return Ok(messages);
        }
    }
}
