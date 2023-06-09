using System.Linq.Expressions;
using MoneyManagement.Domain.Configurations;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Wallets;

namespace MoneyManagement.Service.Interfaces;

public interface IWalletService
{
	public Task<bool> RemoveAsync(long id);
	public Task<WalletResultDto> RetrieveByIdAsync(long id);
	public Task<WalletResultDto> ModifyAsync(WalletForUpdateDto dto);
	public Task<WalletResultDto> CreateAsync(WalletForCreationDto dto);
	public Task<IEnumerable<WalletResultDto>> RetrieveAllAsync(PaginationParams @params);
}
