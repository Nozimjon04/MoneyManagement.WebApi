using Microsoft.AspNetCore.Mvc;
using MoneyManagement.Service.DTOs.Transactions;
using MoneyManagement.Service.DTOs.Users;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.WebApi.Models;

namespace MoneyManagement.WebApi.Controllers
{
	public class TransactionController : BaseController
	{
		private readonly ITransactionService transactionService;

		public TransactionController(ITransactionService transactionService)
		{
			this.transactionService = transactionService;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateTransactionAsync(TransactionForCreationDto dto)
		=> Ok(new Response
		{
			Code = 200,
			Message = "Success",
			Data = await this.transactionService.CreateAsync(dto)
		});

		[HttpPut("Update")]
		public async Task<IActionResult> UpdateTransactionAsync(TransactionForUpdateDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.transactionService.ModifyAsync(dto)
			});

		[HttpGet("get-by-id{id:long}")]
		public async Task<IActionResult> GetByIdAsync(long id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.transactionService.RetrieveByIdAsync(id)
			});

		[HttpGet("get-list")]
		public async Task<IActionResult> GetAllTransactions()
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.transactionService.RetrieveAllAsync()
			});
	}
}
