using System.ComponentModel.DataAnnotations;

namespace clinikpezeshki.Models.Entitys
{
    public class Treatment :DomainEntity
    {

        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(50, ErrorMessage = "حداکثر پنجاه حرف")]

        [Display(Name = "نوع درمان")]
        public string? Name { get; set; }

        public virtual ICollection<Disbursement>? Disbursements { get; set; }


    }
}
