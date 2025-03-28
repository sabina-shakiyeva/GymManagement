namespace FitnessManagement.Entities
{
    public class Workout:BaseEntity
    {
        public string Name { get; set; }
        public DateTime WorkoutDate { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        public ICollection<UserWorkout> UserWorkouts { get; set; }
    }
}
