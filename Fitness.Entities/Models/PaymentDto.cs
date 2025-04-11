namespace FitnessManagement.Dtos
{
    public class PaymentDto
    {
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }

        //public int Id { get; set; }
        //public int UserId { get; set; }
        //public decimal Amount { get; set; }
        //public DateTime PaymentDate { get; set; }
    }
}
