using MoneyManagement.Domain.Commons;

namespace MoneyManagement.Domain.Entities;

public class Reminder : Auditable
{
	public long UserId { get; set; }
	public User User { get; set; }
	public string Description { get; set; }
	public DateTime TargetDate { get; set; }
}
