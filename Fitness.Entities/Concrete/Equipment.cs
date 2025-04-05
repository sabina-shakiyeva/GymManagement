using Fitness.Core.Abstraction;

namespace FitnessManagement.Entities
{
    public class Equipment:BaseEntity, IEntity
    {
        
        public string Name { get; set; }
        public string? Description { get; set; } //  Dumbbell, Treadmill
        public decimal? Price { get; set; }
        public bool IsAvailable { get; set; }
        public decimal? Unit { get; set; }
        public string? ImageUrl { get; set; }
    }
}
