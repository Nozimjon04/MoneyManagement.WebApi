using MoneyManagement.Service.DTOs.Reminders;

namespace MoneyManagement.Service.Interfaces;

public interface IReminderService
{
	public Task<bool> RemoveAsync(long id);
	public Task<ReminderForResultDto> RetrieveById(long id);
	public Task<IEnumerable<ReminderForResultDto>> NotifyUserAsync();
	public Task<IEnumerable<ReminderForResultDto>> RetrieveAllAsync();
	public Task<ReminderForResultDto> ModifyAsync(ReminderForUpdateDto dto);
	public Task<ReminderForResultDto> CreateAsync(ReminderForCreationDto dto);
}
