using System.Linq.Expressions;

namespace UserAuth.Repository.Interface;

public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T model);

    Task UpdateAsync(T model);

    Task DeleteAsync(T model);

    Task<T?> GetByIdAsync(int? Id);

    Task<List<T>> GetAllAsync(params Expression<Func<T, object>>?[]? includes);

    Task<List<T>> GetAllAsync();

    Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        params Expression<Func<T, object>>?[]? includes);
}
