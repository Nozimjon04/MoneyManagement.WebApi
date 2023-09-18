using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Shared.Helpers;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Service.Extensions;
using MoneyManagement.Service.DTOs.Reports;
using MoneyManagement.Domain.Configurations;

namespace MoneyManagement.Service.Services;

public class ReportService : IReportService
{
	private readonly IMapper mapper;
	private readonly IUserService userService;
	private readonly IRepository<Report> reportRepository;

	public ReportService(IMapper mapper,
		IUserService userService,
		IRepository<Report> reportRepository)
	{
		this.mapper = mapper;
		this.userService = userService;
		this.reportRepository = reportRepository;
	}
	public async Task<ReportForResultDto> ModifyAsync(ReportForUpdateDto dto)
	{
		var report = await this.reportRepository.SelectAsync(r => r.Id == dto.Id);
		if (report is null)
			throw new CustomException(404, "Report is not found ");

		var result = this.mapper.Map(dto, report);
		result.UpdateAt = DateTime.UtcNow;
		await this.reportRepository.SaveChangeAsync();

		return this.mapper.Map<ReportForResultDto>(result);
	}

	public async Task<bool> RemoveAsync(long id)
	{
		var report = await this.reportRepository.DeleteAsync(r => r.Id ==id);
		if (!report)
			throw new CustomException(404, "Report is not found");
		await this.reportRepository.SaveChangeAsync();

		return true;
	}

	public async Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync(PaginationParams @params)
	{
		var reports = await this.reportRepository.SelectAllAsync()
			.ToPagedList(@params)
			.ToListAsync();
		return this.mapper.Map<IEnumerable<ReportForResultDto>>(reports);
	}

	public async Task<IEnumerable<ReportForResultDto>> RetrieveUserStatistics()
	{
		var user = await userService.RetrieveByIdAsync(HttpContextHelper.UserId ?? 0);
		var userStatistics = this.reportRepository.SelectAllAsync(us => us.UserId == user.Id);

		return this.mapper.Map<IEnumerable<ReportForResultDto>>(userStatistics);
	}
}
