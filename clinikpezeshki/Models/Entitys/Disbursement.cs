using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace clinikpezeshki.Models.Entitys
{
   
    public class Disbursement:DomainEntity
    {
        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [Display(Name = "هزینه")]
        public long? Cost { get; set; }


       
        public Treatment? Treatment { get; set; }

        [Required]
        public int? TreatmentId { get; set; }


       
        public HowPay? HowPay { get; set; }

        [Required]

        public int? HowPayId { get; set; }


        public Patient? Patient { get; set; }


        [Display(Name = "کد مریض")]

        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [Remote(action: "IsPapientExist",controller: "Disbursements")]
        public int? PatientId { get; set; }

    }
}
