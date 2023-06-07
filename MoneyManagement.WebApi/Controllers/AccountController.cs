using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Service.DTOs.Users;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.WebApi.Models;

namespace MoneyManagement.WebApi.Controllers
{
    public class AccountController : BaseController
	{
		private readonly IUserService userService;
		private readonly IAuthService authService;

		public AccountController(IUserService userService, IAuthService authService)
		{
			this.userService = userService;
			this.authService = authService;
		}

		[HttpPost("Sign-up")]
		public async Task<IActionResult> PostUserAsync(UserForCreationDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.userService.CreateAsync(dto)
			});
		[HttpPost("token")]
		public async Task<IActionResult> GenerateToken(string username, string password = null)
		{
			var token = await this.authService.AuthenticateAsync(username, password);
			return Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = token
			});
		}
	}
}
