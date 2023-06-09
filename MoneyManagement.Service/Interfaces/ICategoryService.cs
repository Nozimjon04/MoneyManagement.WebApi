using MoneyManagement.Domain.Configurations;
using MoneyManagement.Service.DTOs.Categories;

namespace MoneyManagement.Service.Interfaces;

public interface ICategoryService
{
	public Task<bool> RemoveAsync(long Id);
	public Task<CategoryForResultDto> RetrieveByIdAsync(long Id);
	public Task<CategoryForResultDto> ModifyAsync(CategoryForUpdateDto dto);
	public Task<CategoryForResultDto> CreateAsync(CategoryForCreationDto dto);
	public Task<IEnumerable<CategoryForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
