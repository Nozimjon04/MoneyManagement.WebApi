using AutoMapper;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs;

namespace MoneyManagement.Service.Mappers;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		// user
		CreateMap<User, UserForCreationDto>().ReverseMap();
		CreateMap<User, UserResultDto>().ReverseMap();

		// wallet
		CreateMap<Wallet, WalletForCreationDto>().ReverseMap();
		CreateMap<Wallet, WalletResultDto>().ReverseMap();
	}
}
