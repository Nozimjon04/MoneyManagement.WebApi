using MoneyManagement.Domain.Entities;
using MoneyManagement.Domain.Enums;

namespace MoneyManagement.Service.DTOs.Wallets;

public class WalletResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime? TransactionDate { get; set; }
    public TransactionType type { get; set; }
    public string Description { get; set; }
}
