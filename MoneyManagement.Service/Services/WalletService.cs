using AutoMapper;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Interfaces;
using System.Linq.Expressions;

namespace MoneyManagement.Service.Services;

public class WalletService : IWalletService
{
	private readonly IMapper mapper;
	private readonly IRepostory<Wallet> walletRepostory;

	public WalletService(IMapper mapper, IRepostory<Wallet> walletRepostory)
	{
		this.mapper = mapper;
		this.walletRepostory = walletRepostory;
	}

	public async Task<WalletResultDto> CreateAsync(WalletForCreationDto dto)
	{
		var wallet = await this.walletRepostory.SelectAsync(w => w.UserId == dto.UserId);
		if (wallet is not null)
		{
			throw new CustomException(409, "This user has already account in this site");
		}
		var mappedWallet = this.mapper.Map<Wallet>(dto);
		mappedWallet.CreateAt = DateTime.UtcNow;
		var result = await this.walletRepostory.InsertAsync(mappedWallet);
		await this.walletRepostory.SaveChangeAsync();

		return this.mapper.Map<WalletResultDto>(result);
	}

	public async Task<bool> DeleteAsync(long id)
	{
		var result = await this.walletRepostory.DeleteAsync(w => w.Id == id);
		await this.walletRepostory.SaveChangeAsync();

		return result;
	}
	

	public async Task<List<WalletResultDto>> GetAllAsync(Expression<Func<Wallet, bool>> expression = null, string search = null)
	{
		var wallets = this.walletRepostory.SelectAllAsync(expression);
		if (!string.IsNullOrEmpty(search))
		{
			wallets = wallets.Where(w => w.Description.ToLower().Contains(search.ToLower()));
		}
	
		return this.mapper.Map<List<WalletResultDto>>(wallets);
	}

	public async Task<WalletResultDto> GetByIdAsync(long id)
	{
		var wallet = await this.walletRepostory.SelectAsync(w => w.Id == id);
		if (wallet is null)
			throw new CustomException(404, "Wallet is not found ");

		 return this.mapper.Map<WalletResultDto>(wallet);
	}

	public async Task<WalletResultDto> UpdateAsync(long id, WalletForCreationDto dto)
	{
		var wallet = await this.walletRepostory.SelectAsync(w => w.Id == id);
		if (wallet is null)
			throw new CustomException(404, "Wallet is not found ");

		var result = this.mapper.Map(dto, wallet);
		result.Id = id;
		result.UpdateAt = DateTime.UtcNow;
		await this.walletRepostory.SaveChangeAsync();

		return this.mapper.Map<WalletResultDto>(result);
	
	}
}
