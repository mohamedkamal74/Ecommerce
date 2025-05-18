using System.Linq.Expressions;

namespace Ecommerce.core.Interfaces
{
    public interface IGenericRepository<T>where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T,object>>[] includes);
        Task<T> GetById(int id,params Expression<Func<T, object>>[] includes);
        Task<T> GetById(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
