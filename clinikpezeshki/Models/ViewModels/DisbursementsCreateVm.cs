using clinikpezeshki.Models.Entitys;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace clinikpezeshki.Models.ViewModels
{
    public class DisbursementsCreateVm
    {

        public SelectList Treatments { get; init; }
        public SelectList HowPays { get; init; }

        public Disbursement? Disbursement { get; set; }
    }
}
