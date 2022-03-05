using System.ComponentModel.DataAnnotations;

namespace clinikpezeshki.Models.Entitys
{
    public class Reservation: DomainEntity
    {
        [Required(ErrorMessage = "این انتخاب را پرکنید")]
        public int? DoctorId { get; set; }

        public Doctor? Doctor { get; set; }

        [Required(ErrorMessage = "این انتخاب را پرکنید")]
        public int? PatientId { get; set; }

        public Patient? Patient { get; set; }


        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(50, ErrorMessage = "حداکثر پنجاه حرف")]

        [Display(Name = "تاریخ")]
        public string? Time { get; set; }


        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(50, ErrorMessage = "حداکثر پنجاه حرف")]

        [Display(Name = "زمان")]
        public string? date { get; set; }
    }
}
