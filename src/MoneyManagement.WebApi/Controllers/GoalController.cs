using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Service.DTOs.Goals;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.WebApi.Models;
namespace MoneyManagement.WebApi.Controllers
{
	public class GoalController : BaseController
	{
		private readonly IGoalService goalService;

		public GoalController(IGoalService goalService)
		{
			this.goalService = goalService;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateGoalAsync(GoalForCreationDto dto)
		=> Ok(new Response
		{
			Code = 200,
			Message = "Success",
			Data = await this.goalService.CreateAsync(dto)
		});

		[HttpDelete("delete/{id:long}")]
		public async Task<IActionResult> DelateGoalAsync(int id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.goalService.RemoveAsync(id)
			});

		[HttpPut("Update")]
		public async Task<IActionResult> UpdateGoalAsync(GoalForUpdateDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.goalService.ModifyAsync(dto)
			});

		[HttpGet("get-by-id{id:long}")]
		public async Task<IActionResult> GetByIdAsync(long id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.goalService.RetrieveByIdAsync(id)
			});

		[HttpGet("get-list")]
		public async Task<IActionResult> GetAllGoals()
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.goalService.RetrieveAllAsync()
			});

		[HttpGet("get-user-goals")]
		public async Task<IActionResult> GetUserGoals()
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.goalService.RetrieveUserGoals()
			});
	}
}
