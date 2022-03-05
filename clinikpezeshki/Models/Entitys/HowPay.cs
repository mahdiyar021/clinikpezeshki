using System.ComponentModel.DataAnnotations;

namespace clinikpezeshki.Models.Entitys
{
    public class HowPay:DomainEntity
    {
        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(50, ErrorMessage = "حداکثر پنجاه حرف")]

        [Display(Name = "نوع پرداخت")]
        public string? Name { get; set; }

        public virtual ICollection<Disbursement>? Disbursements { get; set; }
    }
}
