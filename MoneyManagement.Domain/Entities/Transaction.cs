using MoneyManagement.Domain.Commons;
using MoneyManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManagement.Domain.Entities;

public class Transaction : Auditable
{
	public long WalletId { get; set; }
	public Wallet Wallet { get; set; }
	public long CategoryId { get; set; }
	public Category Category { get; set; }
	public TransactionType Tye { get; set; }

	[Column(TypeName = "decimal(10,2)")]
	public decimal Amount { get; set; }
	public string Description { get; set; }

}
