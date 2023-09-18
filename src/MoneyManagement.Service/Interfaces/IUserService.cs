using System.Linq.Expressions;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Users;

namespace MoneyManagement.Service.Interfaces;

public interface IUserService
{
	public Task<bool> RemoveAsync(long id);
	Task<User> RetrieveByEmailAsync(string email);
	public Task<UserResultDto> RetrieveByIdAsync(long id);
	public Task<UserResultDto> ModifyAsync(UserForUpdateDto dto);
	public Task<UserResultDto> CreateAsync(UserForCreationDto dto);
	public Task<List<UserResultDto>> RetrieveAllAsync(Expression<Func<User, bool>> expression = null, string search = null);
}
