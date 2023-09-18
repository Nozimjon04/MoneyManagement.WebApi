using Microsoft.AspNetCore.Mvc;
using MoneyManagement.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Service.DTOs.Wallets;
using MoneyManagement.Domain.Configurations;

namespace MoneyManagement.WebApi.Controllers
{
   // [ApiController, Authorize]
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
				Data = await this.walletService.RemoveAsync(id)
			});

		[HttpPut("Update")]
		public async Task<IActionResult> UpdateWalletAsync(WalletForUpdateDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.walletService.ModifyAsync(dto)
			});

		[HttpGet("get-by-id{id:long}")]
		public async Task<IActionResult> GetByIdAsync(long id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.walletService.RetrieveByIdAsync(id)
			});

		[HttpGet("get-list")]
		public async Task<IActionResult> GetAllWallets([FromQuery] PaginationParams @params)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.walletService.RetrieveAllAsync(@params)
			});
	}
}
