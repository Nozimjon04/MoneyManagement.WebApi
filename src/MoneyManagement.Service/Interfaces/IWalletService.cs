using System.Linq.Expressions;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Wallets;

namespace MoneyManagement.Service.Interfaces;

public interface IWalletService
{
	public Task<bool> DeleteAsync(long id);
	public Task<WalletResultDto> GetByIdAsync(long id);
	public Task<WalletResultDto> CreateAsync(WalletForCreationDto dto);
	public Task<WalletResultDto> UpdateAsync(WalletForUpdateDto dto);
	public Task<List<WalletResultDto>> GetAllAsync(Expression<Func<Wallet, bool>> expression = null, string search = null);
}
