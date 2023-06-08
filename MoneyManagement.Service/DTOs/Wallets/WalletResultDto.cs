using MoneyManagement.Domain.Enums;
using MoneyManagement.Domain.Entities;

namespace MoneyManagement.Service.DTOs.Wallets;

public class WalletResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }

}
