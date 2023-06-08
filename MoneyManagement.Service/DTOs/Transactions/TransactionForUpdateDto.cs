using MoneyManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagement.Service.DTOs.Transactions;

public class TransactionForUpdateDto
{
	[Required]
	public long Id { get; set; }

	[Required]
	public long WalletId { get; set; }

	public long CategoryId { get; set; }

	public TransactionType Tye { get; set; }

	public decimal Amount { get; set; }

	public string Description { get; set; }
}
