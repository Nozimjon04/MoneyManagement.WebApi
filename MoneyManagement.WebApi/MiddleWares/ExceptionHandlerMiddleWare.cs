using Microsoft.AspNetCore.Diagnostics;
using MoneyManagement.Service.Exceptions;
using System;

namespace MoneyManagement.WebApi.MiddleWares
{
	public class ExceptionHandlerMiddleWare
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionHandlerMiddleWare> logger;

		public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
		{
			this.next = next;
			this.logger = logger;
		}
		public  async Task  Invoke(HttpContext context)
		{
			try
			{
				await this.next(context);
			}
			catch (CustomException ex)
			{
				context.Response.StatusCode = ex.Code;
				await context.Response.WriteAsJsonAsync(new
				{
					code = ex.Code,
					message = ex.Message,
				});
			}
			catch (Exception ex)
			{
				this.logger.LogError($"{ex.ToString()}\n");
				context.Response.StatusCode = 500;
				await context.Response.WriteAsJsonAsync(new
				{
					code = 500,
					message = ex.Message,
				});
			}
		
		}
	}
}
