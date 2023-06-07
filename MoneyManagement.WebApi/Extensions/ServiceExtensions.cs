using System.Text;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using MoneyManagement.Service.Services;
using MoneyManagement.Data.Repositories;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Data.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MoneyManagement.WebApi.Extensions
{
	public static class ServiceExtensions
	{
		public static void AddCustomService(this IServiceCollection services)
		{
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IWalletService, WalletService>();
			services.AddScoped<ICategoryService,CategoryService>();
			services.AddScoped<ITransactionService, TransactionService>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		}

		public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
				o.SaveToken = true;
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = configuration["JWT:Issuer"],
					ValidAudience = configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Key),
					ClockSkew = TimeSpan.Zero
				};
			});
		}

		public static void AddSwaggerService(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ahsan.Api", Version = "v1" });
				var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Description =
						"JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] { }
				}
			});
			});
		}
	}
}
