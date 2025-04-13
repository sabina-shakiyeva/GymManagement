using Fitness.Core.Abstraction;
using Fitness.Entities.Concrete;

namespace FitnessManagement.Entities
{
    public class TrainerSchedule:BaseEntity,IEntity
    {
        
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //public LessonType LessonType { get; set; }  
        //public string LessonName { get; set; }
    }
    //public enum LessonType
    //{
    //    Group,
    //    Individual
    //}

}
