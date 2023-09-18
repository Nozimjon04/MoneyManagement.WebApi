using MoneyManagement.Domain.Enums;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Wallets;
using MoneyManagement.Service.DTOs.Categories;

namespace MoneyManagement.Service.DTOs.Transactions;

public class TransactionForResultDto
{
	public long Id { get; set; }

	public WalletResultDto wallet { get; set; }

	public CategoryForResultDto Category { get; set; }

	public TransactionType Type { get; set; }

	public decimal Amount { get; set; }

	public string Description { get; set; }
}
