namespace Fitness.Entities.Models.Trainer
{
    public class TrainerScheduleDto
    {
        public int Id { get; set; }
        public int? TrainerId { get; set; }
        public int? UserId { get; set; }
        public int GroupId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
    }
}
