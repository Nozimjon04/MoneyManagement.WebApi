using MoneyManagement.Service.DTOs.Transactions;

namespace MoneyManagement.Service.Interfaces;

public interface ITransactionService
{
	public Task<TransactionForResultDto> RetrieveByIdAsync(long id);
	public Task<IEnumerable<TransactionForResultDto>> RetrieveAllAsync();
	public Task<TransactionForResultDto> ModifyAsync(TransactionForUpdateDto dto);
	public Task<TransactionForResultDto> CreateAsync(TransactionForCreationDto dto);
}
