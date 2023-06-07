using MoneyManagement.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManagement.Domain.Entities;

public class Budget : Auditable
{
	public long WalletId { get; set; }
	public Wallet Wallet { get; set; }
	[Column(TypeName = "decimal(10,2)")]
	public decimal Amount { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
}
