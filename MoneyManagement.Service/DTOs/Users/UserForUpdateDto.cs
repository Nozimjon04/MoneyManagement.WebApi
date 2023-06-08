using MoneyManagement.Domain.Enums;

namespace MoneyManagement.Service.DTOs.Users;

public class UserForUpdateDto
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public GenderType Type { get; set; }
}
