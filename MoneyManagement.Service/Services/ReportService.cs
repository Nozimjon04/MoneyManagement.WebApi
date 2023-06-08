using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Reports;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.Service.Services;

public class ReportService : IReportService
{
	private readonly IMapper mapper;
	private readonly IRepository<Report> reportRepository;

	public ReportService(IMapper mapper, IRepository<Report> reportRepository)
	{
		this.mapper = mapper;
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
		return true;
	}

	public async Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync()
	{
		var reports = await this.reportRepository.SelectAllAsync().ToListAsync();
		return this.mapper.Map<IEnumerable<ReportForResultDto>>(reports);
	}
}
