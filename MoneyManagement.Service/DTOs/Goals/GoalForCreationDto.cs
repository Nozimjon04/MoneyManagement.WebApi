using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MoneyManagement.Service.DTOs.Goals;

public class GoalForCreationDto
{
	public long UserId { get; set; }

	public string Name { get; set; }

	[Column(TypeName = "decimal(10,2)")]
	public decimal TargetAmount { get; set; }

	public DateTime TargetDate { get; set; }
}
