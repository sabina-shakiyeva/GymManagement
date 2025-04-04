namespace FitnessManagement.Dtos
{
    public class PackageDto
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public decimal Price { get; set; }
        public int DurationInMonths { get; set; }
    }
}
