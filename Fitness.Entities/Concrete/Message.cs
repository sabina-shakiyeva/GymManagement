namespace FitnessManagement.Entities
{
    public enum UserType
    {
        User,
        Trainer
    }

    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public UserType SenderType { get; set; }

        public int ReceiverId { get; set; }
        public UserType ReceiverType { get; set; }

        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}
