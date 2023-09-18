using Microsoft.AspNetCore.Mvc;
using MoneyManagement.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using MoneyManagement.Service.DTOs.Users;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.WebApi.Controllers
{
    //[ApiController, Authorize]
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
				Data = await this.userService.RemoveAsync(id)
			});

		[HttpPut("Update")]
		public async Task<IActionResult> UpdateUserAsync(UserForUpdateDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.userService.ModifyAsync(dto)
			});

		[HttpGet("get-by-id{id:long}")]
		public async Task<IActionResult> GetByIdAsync(long id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.userService.RetrieveByIdAsync(id)
			});

		[HttpGet("get-list")]
		public async Task<IActionResult> GetAllUsers()
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.userService.RetrieveAllAsync()
			});
		
			
		
	}
}
