using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinikpezeshki.Models.Entitys
{
    public class Medicine:DomainEntity
    {


        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(50, ErrorMessage = "حداکثر پنجاه حرف")]

        [Display(Name = "نام دارو")]
        public string? Name { get; set; }



        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(50, ErrorMessage = "حداکثر پنجاه حرف")]

        [Display(Name = "نام فارسی دارو")]
        public string? PersionName { get; set; }

        public virtual ICollection<MedicinePrescription>? MedicinePrescriptions { get; set; }
    }
}
