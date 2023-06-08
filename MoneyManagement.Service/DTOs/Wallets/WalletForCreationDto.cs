using MoneyManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MoneyManagement.Service.DTOs.Wallets;

public class WalletForCreationDto
{
    public long UserId { get; set; }

	[Column(TypeName = "decimal(10,2)")]
	public decimal Amount { get; set; }
    public string Description { get; set; }
}
