using MedX.Data.Contexts;
using MedX.Domain.Commons;
using MedX.Data.IRepositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MedX.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly DbSet<T> dbSet;
    private readonly AppDbContext appDbContext;

    public Repository(AppDbContext appDbContext)
    {
        dbSet = appDbContext.Set<T>();
        this.appDbContext = appDbContext;
    }

    public async Task CreateAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        appDbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        entity.IsDeleted = true;
        appDbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Destroy(T entity)
    {
        dbSet.Remove(entity);
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, bool isNoTracked = true, string[] includes = null)
    {
        IQueryable<T> query = expression is null ? dbSet.AsQueryable() : dbSet.Where(expression).AsQueryable();
        query = isNoTracked ? query.AsNoTracking() : query;

        if (includes is not null)
            foreach (var item in includes)
                query = query.Include(item);

        return query;
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null)
    {
        IQueryable<T> query = dbSet.AsQueryable();

        if (includes is not null)
            foreach (var item in includes)
                query = query.Include(item);

        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task SaveChanges()
    {
        await appDbContext.SaveChangesAsync();
    }
}