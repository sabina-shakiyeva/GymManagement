namespace FitnessManagement.Dtos
{
    public class TrainerScheduleDto
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
