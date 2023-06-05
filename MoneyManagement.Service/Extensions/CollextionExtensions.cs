using MoneyManagement.Domain.Commons;
using MoneyManagement.Domain.Configurations;
using MoneyManagement.Service.Exceptions;

namespace MoneyManagement.Service.Extensions;

public static class CollextionExtensions
{
	public static IQueryable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, PaginationParams @params)
			where TEntity : Auditable
	{
		return @params.PageIndex > 0 && @params.PageSize > 0 ?
			entities.OrderBy(e => e.Id)
				.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize) :
					throw new CustomException(400, "Please, enter valid numbers");
	}
}
