using MoneyManagement.Service.DTOs.Reports;
using MoneyManagement.Domain.Configurations;

namespace MoneyManagement.Service.Interfaces;

public interface IReportService
{
	public Task<bool> RemoveAsync(long id);
	public Task<ReportForResultDto> ModifyAsync(ReportForUpdateDto dto);
	public Task<IEnumerable<ReportForResultDto>> RetrieveUserStatistics();
	public Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
