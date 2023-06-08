using MoneyManagement.Domain.Commons;
using MoneyManagement.Domain.Enums;

namespace MoneyManagement.Domain.Entities;

public class Category : Auditable
{
	public string Name { get; set; }

}
