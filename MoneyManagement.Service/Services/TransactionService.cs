using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.DTOs.Transactions;

namespace MoneyManagement.Service.Services;

public class TransactionService : ITransactionService
{
	private readonly IMapper mapper;
	private readonly IRepository<Report> reportRepository;
	private readonly IRepository<Wallet> walletRepository;
	private readonly IRepository<Category> categoryRepository;
	private readonly IRepository<Transaction> transactionRepository;

	public TransactionService(IMapper mapper,
		IRepository<Report> reportRepository,
		IRepository<Wallet> walletRepository,
		IRepository<Category> categoryRepository,
		IRepository<Transaction> transactionRepository)
	{
		this.mapper = mapper;
		this.walletRepository = walletRepository;
		this.categoryRepository = categoryRepository;
		this.transactionRepository = transactionRepository;

	}
	public async Task<TransactionForResultDto> CreateAsync(TransactionForCreationDto dto)
	{
		var wallet = await this.walletRepository.SelectAsync(w => w.Id == dto.WalletId);
		var category = await this.categoryRepository.SelectAsync(c => c.Id == dto.CategoryId);
		// check for existing
		if (wallet is null || category is null)
			throw new CustomException(404, "Wallet or Category is not found");

		var mappedTransaction = this.mapper.Map<Transaction>(dto);
		mappedTransaction.CreateAt = DateTime.UtcNow;
		#region Cheking for valid amount
		if (dto.Type.Equals("income"))
		{
			wallet.Amount += dto.Amount;
		}
		else if (dto.Type.Equals("expense"))
		{
			if(wallet.Amount - dto.Amount > 0)
			{
				wallet.Amount -= dto.Amount;
			}
			else
			{
				throw new CustomException(400, "Amount in Wallet is not enough ");
			}
		}
		#endregion

		var result = await this.transactionRepository.InsertAsync(mappedTransaction);
		await this.transactionRepository.SaveChangeAsync();
		await this.walletRepository.SaveChangeAsync();

		// Writing report once user make transaction for statistics
		Report report = new Report();
		report.UserId = wallet.UserId;
		report.CreateAt = DateTime.UtcNow;
		report.TransactionType = mappedTransaction.Type;
		await this.reportRepository.InsertAsync(report);
		await this.reportRepository.SaveChangeAsync();

		// wallet and category are included in transaction
		result.Wallet = wallet;
		result.Category = category;
		return this.mapper.Map<TransactionForResultDto>(result);
	}

	public async Task<TransactionForResultDto> ModifyAsync(TransactionForUpdateDto dto)
	{
		var transaction = await this.transactionRepository.SelectAsync(t => t.Id== dto.Id);
		var wallet = await this.walletRepository.SelectAsync(w => w.Id == dto.WalletId);
		var category = await this.categoryRepository.SelectAsync(c => c.Id == dto.CategoryId);
		if (wallet is null || category is null  || transaction is null)
			throw new CustomException(404, "Transaction or Wallet or Category is not found ");

		#region Cheking for valid amount
		if (dto.Tye.Equals("income"))
		{
			wallet.Amount += dto.Amount;
		}
		else if (dto.Tye.Equals("expense"))
		{
			if (wallet.Amount - dto.Amount > 0)
			{
				wallet.Amount -= dto.Amount;
			}
			else
			{
				throw new CustomException(400, "Amount in Wallet is not enough ");
			}
		}
		#endregion
		var mappedTransaction = this.mapper.Map(dto, transaction);
		mappedTransaction.UpdateAt = DateTime.UtcNow;
		await this.transactionRepository.SaveChangeAsync();
		await this.walletRepository.SaveChangeAsync();

		return this.mapper.Map<TransactionForResultDto>(mappedTransaction);
	}

	public async Task<IEnumerable<TransactionForResultDto>> RetrieveAllAsync()
	{
		var transactionQuery = this.transactionRepository.SelectAllAsync();
		if (transactionQuery is not null)
		{
			transactionQuery = transactionQuery.Include("Wallet").Include("Category");
		}
		var transactions = await transactionQuery.ToListAsync();
			
		return this.mapper.Map<IEnumerable<TransactionForResultDto>>(transactions);
	}

	public async Task<TransactionForResultDto> RetrieveByIdAsync(long id)
	{
		var transaction = await this.transactionRepository.SelectAsync(t => t.Id == id);
		if (transaction is null)
			throw new CustomException(404, "Transaction is not found");
		transaction.Wallet = await this.walletRepository.SelectAsync(w => w.Id == transaction.WalletId);
		transaction.Category = await this.categoryRepository.SelectAsync(c => c.Id == transaction.CategoryId);

		return this.mapper.Map<TransactionForResultDto>(transaction);
	}
}
 