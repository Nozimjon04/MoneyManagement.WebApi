using MoneyManagement.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyManagement.Domain.Entities;

public class Goal : Auditable
{
	public long UserId { get; set; }
	public User User { get; set; }
	public string Name { get; set; }

	[Column(TypeName = "decimal(10,2)")]
	public decimal TargetAmount { get; set; }
	public DateTime TargetDate { get; set; }
}
