using MoneyManagement.Service.DTOs.Authentifications;

namespace MoneyManagement.Service.Interfaces;

public interface IAuthService
{
	public Task<LoginResultDto> AuthenticateAsync(LoginDto login);
}
