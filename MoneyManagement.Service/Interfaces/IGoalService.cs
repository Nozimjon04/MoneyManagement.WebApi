using MoneyManagement.Service.DTOs.Goals;

namespace MoneyManagement.Service.Interfaces;

public interface IGoalService
{
	public Task<bool> RemoveAsync(long id);
	public Task<GoalForResultDto> RetrieveByIdAsync(long id);
	public Task<GoalForResultDto> ModifyAsync(GoalForUpdateDto dto);
	public Task<IEnumerable<GoalForResultDto>> RetrieveAllAsync();
	public Task<GoalForResultDto> CreateAsync(GoalForCreationDto dto);
}
