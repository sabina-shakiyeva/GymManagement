namespace FitnessManagement.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } //  Dumbbell, Treadmill
        public bool IsAvailable { get; set; }
    }
}
