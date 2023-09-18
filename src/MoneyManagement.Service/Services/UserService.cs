using AutoMapper;
using System.Linq.Expressions;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Service.DTOs.Users;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.Service.Services;

public class UserService : IUserService
{
	private readonly IMapper mapper;
	private readonly IRepository<User> userReposotpry;

	public UserService(IMapper mapper, IRepository<User> userRepository)
	{
		this.mapper = mapper;
		this.userReposotpry = userRepository;
	}
	public async Task<UserResultDto> CreateAsync(UserForCreationDto dto)
	{
		var user = await this.userReposotpry.SelectAsync(u=>u.Email.ToLower()==dto.Email.ToLower());
		if (user is not null)
			throw new CustomException(409, " User is already exists");

		var mappedUser = this.mapper.Map<User>(dto);
		mappedUser.CreateAt = DateTime.UtcNow;
		var result = await this.userReposotpry.InsertAsync(mappedUser);
		await this.userReposotpry.SaveChangeAsync();

		return this.mapper.Map<UserResultDto>(result);
	}

	public async Task<bool> RemoveAsync(long id)
	{
		var result = await this.userReposotpry.DeleteAsync(u => u.Id == id);
		await this.userReposotpry.SaveChangeAsync();

		return result;
	}
	

	public async Task<List<UserResultDto>> RetrieveAllAsync(Expression<Func<User, bool>> expression = null, string search = null)
	{
		var users = this.userReposotpry.SelectAllAsync(expression); 
		if (!string.IsNullOrEmpty(search))
		{
			users = users.Where(u => u.Name.ToLower().Contains(search.ToLower()) ||
				u.Surname.ToLower().Contains(search.ToLower()));
		}

		return this.mapper.Map<List<UserResultDto>>(users);
	}

	public async Task<UserResultDto> RetrieveByIdAsync(long id)
	{
		var user = await this.userReposotpry.SelectAsync(u => u.Id == id);
		if (user is null)
			throw new CustomException(404, "User is not found ");
		var result = this.mapper.Map<UserResultDto>(user);
		

		return result;
	}

	public async Task<User> RetrieveByEmailAsync(string email)
		=> await this.userReposotpry.SelectAsync(u=> u.Email== email);
	

	public async Task<UserResultDto> ModifyAsync(UserForUpdateDto dto)
	{
		var user = await this.userReposotpry.SelectAsync(u => u.Id==dto.Id);
		if (user is null)
			throw new CustomException(404, "User is not found ");
		
		var result = this.mapper.Map(dto, user);
		result.UpdateAt=DateTime.UtcNow;
		await this.userReposotpry.SaveChangeAsync();

		return this.mapper.Map<UserResultDto>(result);
	}
}
