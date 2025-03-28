namespace FitnessManagement.Entities
{
    public class Subscription:BaseEntity
    {
        public string PackageName { get; set; }
        public decimal Price { get; set; }
        public int DurationInMonths { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
