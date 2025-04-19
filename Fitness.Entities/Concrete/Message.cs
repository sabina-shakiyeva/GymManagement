using Fitness.Core.Abstraction;

namespace FitnessManagement.Entities
{
    //public enum UserType
    //{
    //    User,
    //    Trainer
    //}

    public class Message:IEntity
    {
        public int Id { get; set; }
        public string SenderId { get; set; }  // Göndərən istifadəçi (admin və ya digər istifadəçi)
        public string ReceiverId { get; set; } // Alan istifadəçi (qonaq və ya digər istifadəçi)
        public string Content { get; set; }  // Mesajın mətni
        public DateTime SentAt { get; set; } // Göndərilmə vaxtı
    }
}
