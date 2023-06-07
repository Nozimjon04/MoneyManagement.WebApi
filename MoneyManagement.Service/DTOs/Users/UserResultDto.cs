using MoneyManagement.Domain.Entities;

namespace MoneyManagement.Service.DTOs.Users;

public class UserResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<WalletResultDto> Wallets { get; set; }
}
