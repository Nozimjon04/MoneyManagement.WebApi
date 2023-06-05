using MoneyManagement.Domain.Commons;

namespace MoneyManagement.Domain.Entities;

public class User:Auditable
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }

	// EF core realtionship
	public ICollection<Wallet> Wallets { get; set; }

}
