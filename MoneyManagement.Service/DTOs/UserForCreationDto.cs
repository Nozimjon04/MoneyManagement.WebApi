using System.ComponentModel.DataAnnotations;

namespace MoneyManagement.Service.DTOs;

public class UserForCreationDto
{
	public string Name { get; set; }
	public string Surname { get; set; }

	[EmailAddress(ErrorMessage ="email required")]
	public string Email { get; set; }

	[Required(ErrorMessage ="Password is required")]
	public string Password { get; set; }
}
