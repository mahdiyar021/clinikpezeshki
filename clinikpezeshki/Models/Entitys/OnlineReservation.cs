using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace clinikpezeshki.Models.Entitys
{
    public class OnlineReservation: DomainEntity
    {

        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(50, ErrorMessage = "حداکثر پنجاه حرف")]

        [MinLength(3, ErrorMessage = "حداقل 3 حرف")]

        [Display(Name = "نام و نام خانوادگی")]
        public string? FullName { get; set; }



        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [Display(Name = "شماره تماس")]

        [MaxLength(11, ErrorMessage = "حداکثر 11 رقم")]

        [MinLength(11, ErrorMessage = "حداقل 11 رقم")]
        public string? Phonenumber { get; set; }


        [Required(ErrorMessage = "این فیلد را پرکنید")]

        [MaxLength(300, ErrorMessage = "حداکثر 300 حرف")]

        [Display(Name = "زمینه مشاوره")]

        [DataType(DataType.MultilineText)]
        public string? Instroduction { get; set; }
    }
}
