using MoneyManagement.Domain.Enums;
using MoneyManagement.Domain.Entities;

namespace MoneyManagement.Service.DTOs.Reports;

public class ReportForUpdateDto
{
	public long Id { get; set; }

	public long UserId { get; set; }

	public TransactionType TransactionType { get; set; }
}
