using clinikpezeshki.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

public class LoginIndexVm:BaseVm
{

    [Required(ErrorMessage = AttributeConst.RequiredMsg)]

    [MaxLength(AttributeConst.MaxLength, ErrorMessage = AttributeConst.MaxLengthErrorMsg)]

    [MinLength(AttributeConst.MinLength, ErrorMessage = AttributeConst.MinLengthErrorMsg)]

    [Display(Name = DisplayAttributeName.UserName)]

   // [Column(TypeName ="VARCHAR")]

    public string? UserName { get; set; }

    

    [Required(ErrorMessage = AttributeConst.RequiredMsg)]

    [MaxLength(AttributeConst.MaxLength, ErrorMessage = AttributeConst.MaxLengthErrorMsg)]

    [MinLength(AttributeConst.MinLengthPassword, 
        ErrorMessage = AttributeConst.MinLengthPasswordErrorMsg)]

    [Display(Name = DisplayAttributeName.Password)]

    [DataType(DataType.Password)]

  //  [Column(TypeName = "VARCHAR")]
    public string? Password { get; set; }


    
}


