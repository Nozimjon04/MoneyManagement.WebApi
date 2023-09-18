using MoneyManagement.Domain.Commons;
using MoneyManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Transactions;

namespace MoneyManagement.Domain.Entities;

public class Wallet: Auditable
{
	public long UserId { get; set; }
	public User User { get; set; }

	[Column(TypeName = "decimal(10,2)")]
	public decimal Amount { get; set; }
	public string Description { get; set; }

	// EF Core Relationship
	[JsonIgnore]
	public ICollection<Transaction> Transactions { get; set; }

}
