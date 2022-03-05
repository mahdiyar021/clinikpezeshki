using System.ComponentModel.DataAnnotations;

namespace clinikpezeshki.Models.Entitys
{
    
    public class Patient : DomainEntity
    {

        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(50, ErrorMessage = "حداکثر پنجاه حرف")]

        [Display(Name = "نام")]
        public string? Name { get; set; }



        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(50, ErrorMessage = "حداکثر پنجاه حرف")]

        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }


        [Display(Name = "نام و نام خانوادگی")]
        public string? FullName => Name + " " + LastName;

        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [Display(Name = "سن")]

        [Range(8, 250, ErrorMessage = "بین 8 تا 250 سال انتخاب کنید")]
        public byte? Age { get; set; }



        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [Range(0,2)]

        [Display(Name = "جنسیت")]
        
        public byte? Gender { get; set; }



        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [Display(Name = "کد ملی")]
        public string? NationalId { get; set; }



        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [Display(Name = "تلفن")]

        [MaxLength(11, ErrorMessage = "حداکثر 11 رقم")]

        [MinLength(11, ErrorMessage = "حداقل 11 رقم")]
        public string? HomeNumber { get; set; }



        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [Display(Name = "موبایل")]

        [MaxLength(11, ErrorMessage = "حداکثر 11 رقم")]

        [MinLength(11, ErrorMessage = "حداقل 11 رقم")]
        public string? Phonenumber { get; set; }



        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(300, ErrorMessage = "حداکثر 300 حرف")]

        [DataType(DataType.MultilineText)]

        [Display(Name = "آدرس")]
        public string? Adrress { get; set; }



        [Display(Name = "تجویز ها")]
        public virtual ICollection<Prescription>? Prescriptions { get; set; }


        [Display(Name = "هزینه ها")]
        public virtual ICollection<Disbursement>? Disbursements { get; set; }
    }
}
