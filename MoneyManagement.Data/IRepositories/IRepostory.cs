using System.Linq.Expressions;

namespace MoneyManagement.Data.IRepositories;

public interface IRepostory<TEntity>
{
	public Task<bool> SaveChangeAsync(); 
	public Task<TEntity> InsertAsync(TEntity entity);
	public Task<TEntity> UpdateAsync(TEntity entity);
	public Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
	public Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression);
	public IQueryable<TEntity> SelectAllAsync(Expression<Func<TEntity,bool>> expression = null);

}
