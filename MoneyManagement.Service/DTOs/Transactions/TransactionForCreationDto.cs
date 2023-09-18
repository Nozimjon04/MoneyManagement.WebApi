using MoneyManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MoneyManagement.Service.DTOs.Transactions;

public class TransactionForCreationDto
{
	[Required]
	public long WalletId { get; set; }

	[Required]
	public long CategoryId { get; set; }

	public TransactionType Type { get; set; }


	[Column(TypeName = "decimal(10,2)")]
	public decimal Amount { get; set; }

	public string Description { get; set; }
}
