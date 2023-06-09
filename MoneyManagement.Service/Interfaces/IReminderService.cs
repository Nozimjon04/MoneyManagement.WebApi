using MoneyManagement.Domain.Configurations;
using MoneyManagement.Service.DTOs.Reminders;

namespace MoneyManagement.Service.Interfaces;

public interface IReminderService
{
	public Task<bool> RemoveAsync(long id);
	public Task<ReminderForResultDto> RetrieveByIdAsync(long id);
	public Task<IEnumerable<ReminderForResultDto>> NotifyUserAsync();
	public Task<ReminderForResultDto> ModifyAsync(ReminderForUpdateDto dto);
	public Task<ReminderForResultDto> CreateAsync(ReminderForCreationDto dto);
	public Task<IEnumerable<ReminderForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
