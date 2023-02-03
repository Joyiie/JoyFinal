namespace Joy.Infrastructure.ViewModel
{
    public class PPViewModel
    {
        public Guid? PatientId { get; set; }
        public string? Code { get; set; }
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Type? Type { get; set; }
        public Guid? PaymentId { get; set; }
        public string? Tittle { get; set; }
        public string? Description { get; set; }
        public string? Abbrevitation { get; set; }
    }
}
