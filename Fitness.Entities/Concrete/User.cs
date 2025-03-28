using Fitness.Core.Abstraction;
using Microsoft.AspNetCore.Identity;

namespace FitnessManagement.Entities
{
    public class User:BaseEntity,IEntity
    {
       

        public string Name { get; set; }
        public string Email { get; set; }
        public string IdentityUserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsActive { get; set; } = true;

        public bool IsApproved { get; set; } = false;

        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public int? SubscriptionId { get; set; }
        public int? TrainerId { get; set; } 
        public Trainer Trainer { get; set; } 
        public Subscription Subscription { get; set; }

        public ICollection<UserWorkout> UserWorkouts { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Message> Messages { get; set; }

    }

}
