using MoneyManagement.Domain.Commons;
using MoneyManagement.Domain.Enums;
using System.Transactions;

namespace MoneyManagement.Domain.Entities;

public class Wallet: Auditable
{
	public long UserId { get; set; }
	public User User { get; set; }
	public decimal Amount { get; set; }
	public DateTime? TransactionDate { get; set; }
	public TransactionType type { get; set; }
	public string Description { get; set; }

}
