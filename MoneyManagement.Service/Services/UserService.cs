using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs;
using MoneyManagement.Service.DTOs.Users;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Interfaces;
using System.Linq.Expressions;

namespace MoneyManagement.Service.Services;

public class UserService : IUserService
{
	private readonly IMapper mapper;
	private readonly IRepostory<User> userReposotpry;
	private readonly IWalletService walletService;

	public UserService(IMapper mapper, 
		IRepostory<User> userRepository,
		IWalletService walletService)
	{
		this.mapper = mapper;
		this.walletService = walletService;
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

	public async Task<bool> DeleteAsync(long id)
	{
		var result = await this.userReposotpry.DeleteAsync(u => u.Id == id);
		await this.userReposotpry.SaveChangeAsync();

		return result;
	}
	

	public async Task<List<UserResultDto>> GetAllAsync(Expression<Func<User, bool>> expression = null, string search = null)
	{
		var users = this.userReposotpry.SelectAllAsync(expression); 
		if (!string.IsNullOrEmpty(search))
		{
			users = users.Where(u => u.Name.ToLower().Contains(search.ToLower()) ||
				u.Surname.ToLower().Contains(search.ToLower()));
		}

		return this.mapper.Map<List<UserResultDto>>(users);
	}

	public async Task<UserResultDto> GetByIdAsync(long id)
	{
		var user = await this.userReposotpry.SelectAsync(u => u.Id == id);
		if (user is null)
			throw new CustomException(404, "User is not found ");
		var result = this.mapper.Map<UserResultDto>(user);
		result.Wallets = await walletService.GetAllAsync(w => w.UserId == id);

		return result;
	}

	public async Task<User> RetrieveByEmailAsync(string email)
		=> await this.userReposotpry.SelectAsync(u=> u.Email== email);
	

	public async Task<UserResultDto> UpdateAsync(long id, UserForCreationDto dto)
	{
		var user = await this.userReposotpry.SelectAsync(u => u.Id==id);
		if (user is null)
			throw new CustomException(404, "User is not found ");
		
		var result = this.mapper.Map(dto, user);
		result.Id=id;
		result.UpdateAt=DateTime.UtcNow;
		await this.userReposotpry.SaveChangeAsync();

		return this.mapper.Map<UserResultDto>(result);
	}
}
