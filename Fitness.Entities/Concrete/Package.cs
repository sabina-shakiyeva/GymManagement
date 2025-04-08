using Fitness.Core.Abstraction;

namespace FitnessManagement.Entities
{
    public class Package:BaseEntity, IEntity
    {
        public string PackageName { get; set; }
        public decimal Price { get; set; }
        public int DurationInMonths { get; set; }
        public string? Description { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
