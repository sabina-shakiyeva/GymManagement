using Fitness.Business.Abstract;
using FitnessManagement.Entities;
using Microsoft.AspNetCore.SignalR;

namespace FitnessManagement.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessage(int senderId, int receiverId, string content, string senderType, string receiverType)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SenderType = Enum.Parse<UserType>(senderType),
                ReceiverType = Enum.Parse<UserType>(receiverType),
                SentAt = DateTime.UtcNow
            };

            await _messageService.SaveMessageAsync(message);

            // Qarşı tərəfə mesajı göndər
            await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext()?.Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }
    }
}
