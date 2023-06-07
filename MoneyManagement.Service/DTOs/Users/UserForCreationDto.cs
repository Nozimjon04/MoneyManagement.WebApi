using System.ComponentModel.DataAnnotations;

namespace MoneyManagement.Service.DTOs.Users;

public class UserForCreationDto
{
    [Required(ErrorMessage = "You must enter valid name ")]
    [MinLength(2), MaxLength(30)]
    public string Name { get; set; }
    [Required(ErrorMessage = "You must enter the Valid Surname")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "You must enter your email")]
    [EmailAddress(ErrorMessage = "email required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
