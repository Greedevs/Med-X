using MedX.Domain.Commons;
using System.Linq.Expressions;

namespace MedX.Data.IRepositories;

public interface IRepository<T> where T : Auditable
{
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Destroy(T entity);
    Task<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null);
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, bool isNoTracked = true, string[] includes = null);
    Task SaveChanges();
}
