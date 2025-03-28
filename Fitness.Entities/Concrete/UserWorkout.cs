namespace FitnessManagement.Entities
{
    public class UserWorkout
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
