using MoneyManagement.Domain.Enums;
using MoneyManagement.Domain.Entities;

namespace MoneyManagement.Service.DTOs.Transactions;

public class TransactionForResultDto
{
	public long Id { get; set; }

	public Wallet wallet { get; set; }

	public Category Category { get; set; }

	public TransactionType Tye { get; set; }

	public decimal Amount { get; set; }

	public string Description { get; set; }
}
