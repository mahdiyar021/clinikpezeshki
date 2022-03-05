using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinikpezeshki.Models.Entitys
{
    
    public class MedicinePrescription:DomainEntity
    {
        [Required]
        public int? PrescriptionId { get; set; }


        public virtual Prescription? Prescription { get; set; }


        [Required]
        public int? MedicineId { get; set; }


        public virtual Medicine? Medicine { get; set; }
    }
}
