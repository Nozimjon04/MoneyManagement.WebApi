
namespace MoneyManagement.Service.DTOs.Reminders;

public class ReminderForCreationDto
{
	public long UserId { get; set; }

	public string Description { get; set; }

	public DateTime TargetDate { get; set; }
}
