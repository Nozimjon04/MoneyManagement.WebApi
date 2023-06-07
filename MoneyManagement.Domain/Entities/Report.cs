using MoneyManagement.Domain.Commons;
using MoneyManagement.Domain.Enums;

namespace MoneyManagement.Domain.Entities;

public class Report : Auditable
{
	public long UserId { get; set; }
	public User User { get; set; }
	public TransactionType TransactionType { get; set; }
	public DateTime Date { get; set; }
}
