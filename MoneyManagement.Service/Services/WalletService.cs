using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Service.Extensions;
using MoneyManagement.Service.DTOs.Wallets;
using MoneyManagement.Domain.Configurations;

namespace MoneyManagement.Service.Services;

public class WalletService : IWalletService
{
	private readonly IMapper mapper;
	private readonly IUserService userService;
	private readonly IRepository<Wallet> walletRepostory;

	public WalletService(IMapper mapper, 
		IUserService userService,
		IRepository<Wallet> walletRepostory)
	{
		this.mapper = mapper;
		this.userService = userService;
		this.walletRepostory = walletRepostory;
	}

	public async Task<WalletResultDto> CreateAsync(WalletForCreationDto dto)
	{
		var user = await this.userService.RetrieveByIdAsync(dto.UserId);
		if (user is null)
			throw new CustomException(404, "User is not found");
		var mappedWallet = this.mapper.Map<Wallet>(dto);
		mappedWallet.CreateAt = DateTime.UtcNow;
		var result = await this.walletRepostory.InsertAsync(mappedWallet);
		await this.walletRepostory.SaveChangeAsync();

		return this.mapper.Map<WalletResultDto>(result);
	}

	public async Task<bool> RemoveAsync(long id)
	{
		var result = await this.walletRepostory.DeleteAsync(w => w.Id == id);
		await this.walletRepostory.SaveChangeAsync();

		return result;
	}
	

	public async Task<IEnumerable<WalletResultDto>> RetrieveAllAsync(PaginationParams @params)
	{
		var wallets = await this.walletRepostory.SelectAllAsync()
			.ToPagedList(@params)
			.ToListAsync();
	
		return this.mapper.Map<IEnumerable<WalletResultDto>>(wallets);
	}

	public async Task<WalletResultDto> RetrieveByIdAsync(long id)
	{
		var wallet = await this.walletRepostory.SelectAsync(w => w.Id == id);
		if (wallet is null)
			throw new CustomException(404, "Wallet is not found ");

		 return this.mapper.Map<WalletResultDto>(wallet);
	}

	public async Task<WalletResultDto> ModifyAsync(WalletForUpdateDto dto)
	{
		var wallet = await this.walletRepostory.SelectAsync(w => w.Id == dto.Id);
		var user = await this.userService.RetrieveByIdAsync(dto.UserId);
		if (wallet is null || user is null)
			throw new CustomException(404, "Wallet is not found ");

		var result = this.mapper.Map(dto, wallet);
		result.UpdateAt = DateTime.UtcNow;
		await this.walletRepostory.SaveChangeAsync();

		return this.mapper.Map<WalletResultDto>(result);
	
	}
}
