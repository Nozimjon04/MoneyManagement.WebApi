using Microsoft.AspNetCore.Mvc;
using MoneyManagement.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Service.DTOs.Wallets;

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
		public async Task<IActionResult> UpdateWalletAsync(WalletForUpdateDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.walletService.UpdateAsync(dto)
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
