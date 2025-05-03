using Ecommerce.core.Interfaces;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id, params Expression<Func<T, object>>[] includes)
        { 
           IQueryable<T> query =  _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var entity=await query.FirstOrDefaultAsync(x=> EF.Property<int>(x,"Id" ) ==id);
            return entity;
        }

        public async Task<T> GetById(int id) =>  await _context.Set<T>().FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State= EntityState.Modified;
          await  _context.SaveChangesAsync();
        }
    }
}
