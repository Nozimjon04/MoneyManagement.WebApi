using MoneyManagement.Service.DTOs;

namespace MoneyManagement.Service.Interfaces;

public interface IAuthService
{
	public Task<LoginResultDto> AuthenticateAsync(string email, string password);
}
