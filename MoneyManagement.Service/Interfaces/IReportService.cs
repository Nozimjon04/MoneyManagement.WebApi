using MoneyManagement.Service.DTOs.Reports;

namespace MoneyManagement.Service.Interfaces;

public interface IReportService
{
	public Task<bool> RemoveAsync(long id);
	public Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync();
	public Task<ReportForResultDto> ModifyAsync(ReportForUpdateDto dto);
}
