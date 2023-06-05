using Microsoft.EntityFrameworkCore;
using MoneyManagement.Data.Contexts;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Domain.Commons;
using System.Linq.Expressions;

namespace MoneyManagement.Data.Repositories;

public class Repository<TEntity> : IRepostory<TEntity> where TEntity : Auditable
{
	private readonly AppDbContext dbContext;
	private readonly DbSet<TEntity> dbSet;

	public Repository(AppDbContext dbContext)
	{
		this.dbContext = dbContext;
		this.dbSet = dbContext.Set<TEntity>();
	}

	public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
	{
		var exsistEntity =  await dbSet.FirstOrDefaultAsync(expression);
		if (exsistEntity is null)
		{
			return false;
		}

		dbSet.Remove(exsistEntity);
		return true;
	}

	public async Task<TEntity> InsertAsync(TEntity entity)
		=> (await dbSet.AddAsync(entity)).Entity;
	
	

	public async Task<bool> SaveChangeAsync()
	{
		return await this.dbContext.SaveChangesAsync()>0;
	}
	

	public IQueryable<TEntity> SelectAllAsync(Expression<Func<TEntity, bool>> expression)
	{
		var query = expression is null ? dbSet : dbSet.Where(expression);
		return query;
	}

	public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression)
	{
		return await this.SelectAllAsync(expression).FirstOrDefaultAsync();
	}

	public async Task<TEntity> UpdateAsync(TEntity entity)
		=> (this.dbSet.Update(entity)).Entity;
}
