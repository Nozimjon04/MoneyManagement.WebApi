using AutoMapper;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Users;
using MoneyManagement.Service.DTOs.Wallets;
using MoneyManagement.Service.DTOs.Transactions;
using MoneyManagement.Service.DTOs.Categories;
using MoneyManagement.Service.DTOs.Reports;
using MoneyManagement.Service.DTOs.Reminders;
using MoneyManagement.Service.DTOs.Goals;

namespace MoneyManagement.Service.Mappers;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		// user
		CreateMap<User, UserResultDto>().ReverseMap();
		CreateMap<User, UserForUpdateDto>().ReverseMap();
		CreateMap<User, UserForCreationDto>().ReverseMap();

		// goal
		CreateMap<Goal,GoalForResultDto>().ReverseMap();
		CreateMap<Goal, GoalForUpdateDto>().ReverseMap();
		CreateMap<Goal, GoalForCreationDto>().ReverseMap();

		//report
		CreateMap<Report, ReportForResultDto>().ReverseMap();
		CreateMap<Report, ReportForResultDto>().ReverseMap();

		// wallet
		CreateMap<Wallet, WalletResultDto>().ReverseMap();
		CreateMap<Wallet, WalletForUpdateDto>().ReverseMap();
		CreateMap<Wallet, WalletForCreationDto>().ReverseMap();

		// reminder
		CreateMap<Reminder, ReminderForUpdateDto>().ReverseMap();
		CreateMap<Reminder, ReminderForResultDto>().ReverseMap();
		CreateMap<Reminder, ReminderForCreationDto>().ReverseMap();

		// category 
		CreateMap<Category, CategoryForResultDto>().ReverseMap();
		CreateMap<Category, CategoryForUpdateDto>().ReverseMap();
		CreateMap<Category, CategoryForCreationDto>().ReverseMap();

		// transaction
		CreateMap<Transaction, TransactionForResultDto>().ReverseMap();
		CreateMap<Transaction, TransactionForUpdateDto>().ReverseMap();
		CreateMap<Transaction, TransactionForCreationDto>().ReverseMap();
	}
}
