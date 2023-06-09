﻿using Microsoft.AspNetCore.Mvc;
using MoneyManagement.WebApi.Models;
using MoneyManagement.Service.Interfaces;
using MoneyManagement.Service.DTOs.Categories;
using MoneyManagement.Domain.Configurations;

namespace MoneyManagement.WebApi.Controllers
{
	public class CategoryController : BaseController
	{
		private readonly ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateCategoryAsync(CategoryForCreationDto dto)
		=> Ok(new Response
		{
			Code = 200,
			Message = "Success",
			Data = await this.categoryService.CreateAsync(dto)
		});

		[HttpDelete("delete/{id:long}")]
		public async Task<IActionResult> DelateCategoryAsync(int id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.categoryService.RemoveAsync(id)
			});

		[HttpPut("Update")]
		public async Task<IActionResult> UpdateCategoryAsync(CategoryForUpdateDto dto)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.categoryService.ModifyAsync(dto)
			});

		[HttpGet("get-by-id{id:long}")]
		public async Task<IActionResult> GetByIdAsync(long id)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.categoryService.RetrieveByIdAsync(id)
			});

		[HttpGet("get-list")]
		public async Task<IActionResult> GetAllCategories([FromQuery] PaginationParams @params)
			=> Ok(new Response
			{
				Code = 200,
				Message = "Success",
				Data = await this.categoryService.RetrieveAllAsync(@params)
			});
	}
}
