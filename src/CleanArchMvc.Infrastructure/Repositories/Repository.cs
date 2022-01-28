using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces.Repositories;
using CleanArchMvc.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entity;

        protected Repository(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entity.AsNoTracking()
                                    .ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _entity.FindAsync(id);
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            _entity.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _entity.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task RemoveAsync(T entity)
        {
            _entity.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}