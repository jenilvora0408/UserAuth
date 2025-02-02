using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserAuth.Entities.Data;
using UserAuth.Repository.Interface;

namespace UserAuth.Repository.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T model)
    {
        await _dbSet.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T model)
    {
        _dbSet.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T model)
    {
        _dbSet.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync(int? Id)
    {
        return await _dbSet.FindAsync(Id);
    }

    public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>?[]? includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        params Expression<Func<T, object>>?[]? includes)
    {
        IQueryable<T> query = _dbSet;

        // Apply Includes
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        // Apply Filtering
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Apply Sorting
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.ToListAsync();
    }

}
