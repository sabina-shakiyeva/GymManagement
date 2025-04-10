using FitnessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Abstract
{
    public interface IMessageService
    {
        Task SaveMessageAsync(Message message);
        Task<List<Message>> GetChatHistoryAsync(int userId, int receiverId);
    }
}
