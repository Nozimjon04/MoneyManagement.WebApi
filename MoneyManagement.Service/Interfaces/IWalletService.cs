using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs;
using System.Linq.Expressions;

namespace MoneyManagement.Service.Interfaces;

public interface IWalletService
{
	public Task<WalletResultDto> CreateAsync(WalletForCreationDto dto);
	public Task<WalletResultDto> UpdateAsync(long id, WalletForCreationDto dto);
	public Task<bool> DeleteAsync(long id);
	public Task<WalletResultDto> GetByIdAsync(long id);
	public Task<List<WalletResultDto>> GetAllAsync(Expression<Func<Wallet, bool>> expression = null, string search = null);
}
