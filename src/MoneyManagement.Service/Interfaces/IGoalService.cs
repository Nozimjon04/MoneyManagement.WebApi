using MoneyManagement.Service.DTOs.Goals;
using MoneyManagement.Domain.Configurations;

namespace MoneyManagement.Service.Interfaces;

public interface IGoalService
{
	public Task<bool> RemoveAsync(long id);
	public Task<GoalForResultDto> RetrieveByIdAsync(long id);
	public Task<IEnumerable<GoalForResultDto>> RetrieveUserGoals();
	public Task<GoalForResultDto> ModifyAsync(GoalForUpdateDto dto);
	public Task<GoalForResultDto> CreateAsync(GoalForCreationDto dto);
	public Task<IEnumerable<GoalForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
