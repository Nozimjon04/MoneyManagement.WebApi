using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Shared.Helpers;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Service.DTOs.Reminders;

namespace MoneyManagement.Service.Services;

public class ReminderService : IReminderService
{
	private readonly IMapper mapper;
	private readonly IUserService userService;
	private readonly IRepository<Reminder> reminderRepository;

	public ReminderService(IMapper mapper,
		IUserService userService,
		IRepository<Reminder> reminderRepository)
	{
		this.mapper = mapper;
		this.userService = userService;
		this.reminderRepository = reminderRepository;
	}

	public async Task<ReminderForResultDto> CreateAsync(ReminderForCreationDto dto)
	{
		var user = await this.userService.RetrieveByIdAsync(dto.UserId);
		if (user is null)
			throw new CustomException(404, "User is not found");

		var mappedReminder = this.mapper.Map<Reminder>(dto);
		mappedReminder.CreateAt = DateTime.UtcNow;
		var result = await this.reminderRepository.InsertAsync(mappedReminder);
		await this.reminderRepository.SaveChangeAsync();

		return this.mapper.Map<ReminderForResultDto>(result);
	}

	public async Task<ReminderForResultDto> ModifyAsync(ReminderForUpdateDto dto)
	{
		var reminder = await this.reminderRepository.SelectAsync(r => r.Id == dto.Id);
		if (reminder is null)
			throw new CustomException(404, "Reminder is not found");
		var mappedReminder = this.mapper.Map(dto, reminder);
		mappedReminder.UpdateAt = DateTime.UtcNow;
		await this.reminderRepository.SaveChangeAsync();

		return this.mapper.Map<ReminderForResultDto>(mappedReminder);
	}

	public async Task<IEnumerable<ReminderForResultDto>> NotifyUserAsync()
	{
		var reminders = await this.reminderRepository.SelectAllAsync(r => r.UserId == HttpContextHelper.UserId && r.TargetDate<DateTime.UtcNow).ToListAsync();
		return this.mapper.Map<IEnumerable<ReminderForResultDto>>(reminders);
	}

	public async Task<bool> RemoveAsync(long id)
	{
		bool reminder = await this.reminderRepository.DeleteAsync(r => r.Id == id);
		if (!reminder)
			throw new CustomException(404, "Reminder is not found");
		return reminder;
	}

	public async Task<IEnumerable<ReminderForResultDto>> RetrieveAllAsync()
	{
		var reminders = await this.reminderRepository.SelectAllAsync().ToListAsync();
		return this.mapper.Map<IEnumerable<ReminderForResultDto>>(reminders);
	}

	public async Task<ReminderForResultDto> RetrieveByIdAsync(long id)
	{
		var reminder = await this.reminderRepository.SelectAsync(r => r.Id == id);
		if (reminder is null)
			throw new CustomException(404, "Reminder is not found");
		return this.mapper.Map<ReminderForResultDto>(reminder);
	}
}
