using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinikpezeshki.Models.Entitys
{
    public class Expertise:DomainEntity
    {
        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(50, ErrorMessage = "حداکثر پنجاه حرف")]

        [Display(Name = "تخصص")]
        public string? Name { get; set; }


        public virtual ICollection<Doctor>? Doctors { get; set; }
    }
}
