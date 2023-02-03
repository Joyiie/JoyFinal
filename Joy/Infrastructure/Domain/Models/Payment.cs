namespace Joy.Infrastructure.Domain.Models
{
    public class Payment
    {
       public Guid? PaymentId { get; set; }
       public string? Tittle { get; set; }
       public string? Description { get;set; }
       public string? Abbrevitation { get; set; }
    }
}
