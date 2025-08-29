using System.ComponentModel.DataAnnotations;



public class LoginVm
{
  
    [Required(ErrorMessage = "Please enter user name.")]
    public string Name { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}