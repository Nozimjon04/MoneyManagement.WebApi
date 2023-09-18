using MoneyManagement.Domain.Enums;
using MoneyManagement.Domain.Commons;
using System.Text.Json.Serialization;

namespace MoneyManagement.Domain.Entities;

public class User:Auditable
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public GenderType Type { get; set; }

	// EF core realtionship
	[JsonIgnore]
	public ICollection<Wallet> Wallets { get; set; }
	public ICollection<Goal> Goals { get; set; }
	public ICollection<Reminder> Reminders { get; set; }
	public ICollection<Report> Reports { get; set; }

}
