namespace MoneyManagement.Service.DTOs.Reminders;

public class ReminderForResultDto
{
	public long Id { get; set; }

	public long UserId { get; set; }

	public string Description { get; set; }

	public DateTime TargetDate { get; set; }
}
