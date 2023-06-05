using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Service.DTOs;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.WebApi.Models;

namespace MoneyManagement.WebApi.Controllers
{
	[ApiController, Authorize]
	public class WalletController : BaseController
	{
		private readonly IWalletService walletService;

		public WalletController(IWalletService walletService)
		{
			this.walletService = walletService;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateWalletAsync(WalletForCreationDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.walletService.CreateAsync(dto)
			});

		[HttpDelete("delete/{id:long}")]
		public async Task<IActionResult> DelateWalletAsync(int id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.walletService.DeleteAsync(id)
			});

		[HttpPut("Update")]
		public async Task<IActionResult> UpdateWalletAsync(long id, WalletForCreationDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.walletService.UpdateAsync(id, dto)
			});

		[HttpGet("get-by-id{id:long}")]
		public async Task<IActionResult> GetByIdAsync(long id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.walletService.GetByIdAsync(id)
			});

		[HttpGet("get-list")]
		public async Task<IActionResult> GetAllWallets()
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.walletService.GetAllAsync()
			});
	}
}
