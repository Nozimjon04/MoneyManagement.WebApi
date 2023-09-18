using Microsoft.AspNetCore.Mvc;
using MoneyManagement.WebApi.Models;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Service.DTOs.Users;
using MoneyManagement.Service.DTOs.Authentifications;

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
		public async Task<IActionResult> GenerateToken(LoginDto login)
		{
			var token = await this.authService.AuthenticateAsync(login);
			return Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = token
			});
		}
	}
}
