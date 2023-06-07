using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs;
using MoneyManagement.Service.DTOs.Users;
using System.Linq.Expressions;

namespace MoneyManagement.Service.Interfaces;

public interface IUserService
{
	public Task<UserResultDto> CreateAsync(UserForCreationDto dto);
	public Task<UserResultDto> UpdateAsync(long id,UserForCreationDto dto);
	public Task<bool> DeleteAsync(long id);
	public Task<UserResultDto> GetByIdAsync(long id);
	Task<User> RetrieveByEmailAsync(string email);
	public Task<List<UserResultDto>> GetAllAsync(Expression<Func<User, bool>> expression = null, string search = null);
}
