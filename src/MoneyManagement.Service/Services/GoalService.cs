using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Shared.Helpers;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Service.DTOs.Goals;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.Service.Services;

public class GoalService : IGoalService
{
	private readonly IMapper mapper;
	private readonly IUserService userService;
	private readonly IRepository<Goal> goalRepository;

	public GoalService(IMapper mapper,
		IUserService userService,
		IRepository<Goal> goalRepository)
	{
		this.mapper = mapper;
		this.userService = userService;
		this.goalRepository = goalRepository;
	}

	public async Task<GoalForResultDto> CreateAsync(GoalForCreationDto dto)
	{
		var user = await this.userService.RetrieveByIdAsync(dto.UserId);
		if (user is null)
			throw new CustomException(404, "User is not found");

		var mappedGoal = this.mapper.Map<Goal>(dto);
		mappedGoal.CreateAt = DateTime.UtcNow;
		var result = await this.goalRepository.InsertAsync(mappedGoal);
		await this.goalRepository.SaveChangeAsync();

		return this.mapper.Map<GoalForResultDto>(result);
	}

	public async Task<GoalForResultDto> ModifyAsync(GoalForUpdateDto dto)
	{
		var goal = await this.goalRepository.SelectAsync(g => g.Id == dto.Id);
		if (goal is null)
			throw new CustomException(404, "Goal is not found");

		var mappedGoal = this.mapper.Map(dto, goal);
		mappedGoal.UpdateAt = DateTime.UtcNow;
		await this.goalRepository.SaveChangeAsync();

		return this.mapper.Map<GoalForResultDto>(mappedGoal);
	}

	public async Task<bool> RemoveAsync(long id)
	{
		bool goal = await this.goalRepository.DeleteAsync(g => g.Id == id);
		if (!goal)
			throw new CustomException(404, "Goal is not found");
		await this.goalRepository.SaveChangeAsync();

		return true;
	}

	public async Task<IEnumerable<GoalForResultDto>> RetrieveAllAsync()
	{
		var goals = await this.goalRepository.SelectAllAsync().ToListAsync();
		return this.mapper.Map<IEnumerable<GoalForResultDto>>(goals);
	}

	public async Task<GoalForResultDto> RetrieveByIdAsync(long id)
	{
		var goal = await this.goalRepository.SelectAsync(g=> g.Id == id);
		if (goal is null)
			throw new CustomException(404, "Goal is not found");
		return this.mapper.Map<GoalForResultDto>(goal);
	}

	public async Task<IEnumerable<GoalForResultDto>> RetrieveUserGoals()
	{
		var user = await userService.RetrieveByIdAsync(HttpContextHelper.UserId ?? 0);
		var goals = this.goalRepository.SelectAllAsync(g => g.UserId == user.Id && g.TargetDate >= DateTime.UtcNow);

		return this.mapper.Map<IEnumerable<GoalForResultDto>>(goals);
	}
}
