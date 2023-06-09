using Microsoft.AspNetCore.Mvc;
using MoneyManagement.WebApi.Models;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Service.DTOs.Reports;
using MoneyManagement.Domain.Configurations;

namespace MoneyManagement.WebApi.Controllers
{
	public class ReportController : BaseController
	{
		private readonly IReportService reportService;

		public ReportController(IReportService reportService)
		{
			this.reportService = reportService;
		}


		[HttpDelete("delete/{id:long}")]
		public async Task<IActionResult> DelateReportAsync(int id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.reportService.RemoveAsync(id)
			});

		[HttpPut("Update")]
		public async Task<IActionResult> UpdateReportAsync(ReportForUpdateDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.reportService.ModifyAsync(dto)
			});

		[HttpGet("get-list")]
		public async Task<IActionResult> GetAllReports([FromQuery] PaginationParams @params)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.reportService.RetrieveAllAsync(@params)
			});

		[HttpGet("get-user-statistics")]
		public async Task<IActionResult> RetrieveUserStatistics()
			=> Ok( new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.reportService.RetrieveUserStatistics()
			});
	}
}
