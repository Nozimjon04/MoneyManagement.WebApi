using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Data.Contexts;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Service.Mappers;
using MoneyManagement.WebApi.Extensions;
using MoneyManagement.WebApi.Helpers;
using MoneyManagement.WebApi.MiddleWares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCustomService();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option =>
				option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add MappingProfile
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();


//Convert  Api url name to dash case 
builder.Services.AddControllers(options =>
	options.Conventions.Add(
		new RouteTokenTransformerConvention(new RouteConfiguration())));

// swagger set up
builder.Services.AddSwaggerService();
// JWT service
builder.Services.AddJwtService(builder.Configuration);

// Logger
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
var app = builder.Build();

app.InitAccessor();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleWare>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
