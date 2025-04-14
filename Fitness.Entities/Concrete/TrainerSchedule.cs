using Fitness.Core.Abstraction;
using Fitness.Entities.Concrete;

namespace FitnessManagement.Entities
{
    public class TrainerSchedule:BaseEntity,IEntity
    {

        public int UserId { get; set; }             
        public User User { get; set; }               

        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string? Description { get; set; }

    }
   

}
