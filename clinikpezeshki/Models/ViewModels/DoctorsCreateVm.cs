using clinikpezeshki.Models.Entitys;
using Microsoft.AspNetCore.Mvc.Rendering;

public class DoctorsCreateVm
{
    public Doctor? Doctor { get; set; }

    public SelectList? Expertises { get; set; }

}


