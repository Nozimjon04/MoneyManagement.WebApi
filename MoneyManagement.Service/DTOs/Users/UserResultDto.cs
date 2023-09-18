using MoneyManagement.Domain.Entities;
using MoneyManagement.Domain.Enums;
using MoneyManagement.Service.DTOs.Wallets;

namespace MoneyManagement.Service.DTOs.Users;

public class UserResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
	public GenderType Type { get; set; }
}
