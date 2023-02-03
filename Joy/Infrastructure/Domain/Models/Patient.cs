using System.ComponentModel.DataAnnotations.Schema;

namespace Joy.Infrastructure.Domain.Models
{
    public class Patient
    {
        public Guid? PatientId { get; set; }
        public  string? Code { get; set; }
        public  string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Type ? Type { get; set; }
        public Guid? PaymentId { get; set; }


        [ForeignKey("PaymetId")]
        public Payment? Payment { get; set; }

    }

    public enum Type
    {
        Chemo =1,
        Generic =2,
        Procedure =3,
        Protocol = 4
    }
}

