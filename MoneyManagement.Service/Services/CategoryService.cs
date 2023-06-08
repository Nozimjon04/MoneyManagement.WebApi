using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Service.DTOs.Categories;
using MoneyManagement.Service.Exceptions;
using MoneyManagement.Service.Interfaces;

namespace MoneyManagement.Service.Services;

public class CategoryService : ICategoryService
{
	private IMapper mapper;
	private readonly IRepository<Category> categoryRepository;

	public CategoryService(IMapper mapper, IRepository<Category> categoryRepository)
	{
		this.mapper = mapper;
		this.categoryRepository = categoryRepository;
	}
	public async Task<CategoryForResultDto> CreateAsync(CategoryForCreationDto dto)
	{
		var mappedCategory = this.mapper.Map<Category>(dto);
		mappedCategory.CreateAt = DateTime.UtcNow;
		var result = await this.categoryRepository.InsertAsync(mappedCategory);

		return this.mapper.Map<CategoryForResultDto>(result);
	}

	public async Task<CategoryForResultDto> ModifyAsync(CategoryForUpdateDto dto)
	{
		var category = await this.categoryRepository.SelectAsync(c => c.Id == dto.Id);
		if (category is null)
			throw new CustomException(404, "Category is not found");
		var mappedCategory = this.mapper.Map(dto, category);
		mappedCategory.UpdateAt = DateTime.UtcNow;

		return this.mapper.Map<CategoryForResultDto>(mappedCategory);
	}

	public async Task<bool> RemoveAsync(long Id)
	{
		var category = await categoryRepository.DeleteAsync(c => c.Id == Id);
		if (!category)
			throw new CustomException(404, "Category is not found");
		return true;
	}

	public  async Task<IEnumerable<CategoryForResultDto>> RetrieveAllAsync()
	{
		var categories =  await this.categoryRepository.SelectAllAsync().ToListAsync();
		return this.mapper.Map<IEnumerable<CategoryForResultDto>>(categories);
	}

	public async Task<CategoryForResultDto> RetrieveByIdAsync(long Id)
	{
		var category = await this.categoryRepository.SelectAsync(c => c.Id == Id);
		if (category is null)
			throw new CustomException(404, "Category is not found");

		return this.mapper.Map<CategoryForResultDto>(category);
	}
}
