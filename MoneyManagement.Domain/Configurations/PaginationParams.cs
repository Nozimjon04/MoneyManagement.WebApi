namespace MoneyManagement.Domain.Configurations;

public class PaginationParams
{
	private int pageSize;
	private const int maxPageSize = 10;

	public int PageSize
	{
		get => pageSize;
		set => pageSize = value > maxPageSize? maxPageSize : value;
	}
	public int PageIndex { get; set; }
}
