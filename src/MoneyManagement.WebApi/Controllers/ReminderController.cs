using Microsoft.AspNetCore.Mvc;
using MoneyManagement.WebApi.Models;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Service.DTOs.Reminders;

namespace MoneyManagement.WebApi.Controllers
{
	public class ReminderController : BaseController
	{
		private readonly IReminderService reminderService;

		public ReminderController(IReminderService reminderService)
		{
			this.reminderService = reminderService;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateReminderAsync(ReminderForCreationDto dto)
		=> Ok(new Response
		{
			Code = 200,
			Message = "Success",
			Data = await this.reminderService.CreateAsync(dto)
		});

		[HttpDelete("delete/{id:long}")]
		public async Task<IActionResult> DelateReminderAsync(int id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.reminderService.RemoveAsync(id)
			});

		[HttpPut("Update")]
		public async Task<IActionResult> UpdateReminderAsync(ReminderForUpdateDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.	reminderService.ModifyAsync(dto)
			});

		[HttpGet("get-by-id{id:long}")]
		public async Task<IActionResult> GetByIdAsync(long id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.reminderService.RetrieveByIdAsync(id)
			});

		[HttpGet("get-list")]
		public async Task<IActionResult> GetAllReminders()
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.reminderService.RetrieveAllAsync()
			});
		[HttpGet("get-notifications")]
		public async Task<IActionResult> NotifyUser()
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.reminderService.NotifyUserAsync()
			});
	}
}
