using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinikpezeshki.Models.Entitys
{
    public class Prescription : DomainEntity
    {

        [Display(Name = "تاریخ")]

        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(30, ErrorMessage = "حداکثر 30 حرف")]
        public string? DateTime { get; set; }


        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(300, ErrorMessage = "حداکثر 300 حرف")]

        [Display(Name = "دستور مصرف")]

        [DataType(DataType.MultilineText)]
        public string? InstroductionForUse { get; set; }



        public virtual ICollection<MedicinePrescription>? MedicinePrescriptions { get; set; }


        public Patient? Patient { get; set; }

        [Required]
        public int? PatientId { get; set; }


        public Doctor? Doctor { get; set; }

        [Required]
        public int? DoctorId { get; set; }

    }

    public class HasMedicine
    {
        public int? MedicineId { get; set; }

        public bool IsHasMedicine { get; set; }
    }
}
