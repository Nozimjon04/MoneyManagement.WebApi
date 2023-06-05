using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Service.DTOs;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.WebApi.Models;

namespace MoneyManagement.WebApi.Controllers
{
	[ApiController, Authorize]
	public class UserController : BaseController
	{
		private readonly IUserService userService;
		public UserController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateUserAsync(UserForCreationDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.userService.CreateAsync(dto)
			});
		[HttpDelete("delete/{id:long}")]
		public async Task<IActionResult> DelateUserAsync(int id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.userService.DeleteAsync(id)
			});
		[HttpPut("Update")]
		public async Task<IActionResult> UpdateUserAsync(long id, UserForCreationDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.userService.UpdateAsync(id,dto)
			});
		[HttpGet("get-by-id{id:long}")]
		public async Task<IActionResult> GetByIdAsync(long id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.userService.GetByIdAsync(id)
			});
		[HttpGet("get-list")]
		public async Task<IActionResult> GetAllUsers()
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.userService.GetAllAsync()
			});
		
			
		
	}
}
