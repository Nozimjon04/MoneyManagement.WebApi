using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoneyManagement.Service.Services;

public class AuthService : IAuthService
{
	private readonly IUserService userService;
	private readonly IConfiguration configuration;
	public AuthService(IUserService userService, IConfiguration configuration)
	{
		this.userService = userService;
		this.configuration = configuration;
	}
	public async Task<LoginResultDto> AuthenticateAsync(string email, string password)
	{
		var user = await this.userService.RetrieveByEmailAsync(email);
		if (user is null)
			throw new CustomException(404, "Email is incorrect ");
		return new LoginResultDto
		{
			Token = GenerateToken(user)
		};
	}

	public string GenerateToken(User user)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new Claim[]
			{
				 new Claim("Id", user.Id.ToString()),
				 new Claim(ClaimTypes.Email, user.Email)
			}),
			Audience = configuration["JWT:Audience"],
			Issuer = configuration["JWT:Issuer"],
			IssuedAt = DateTime.UtcNow,
			Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["JWT:Expire"])),
			SigningCredentials = new SigningCredentials(
				new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}
