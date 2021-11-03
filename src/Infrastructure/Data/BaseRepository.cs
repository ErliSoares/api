using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    /// <inheritdoc />
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dataset;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        /// <inheritdoc />
        public async Task<bool> Delete(int id)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            if (result == null)
                return false;

            _dataset.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<T> Insert(T item)
        {
            _dataset.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        /// <inheritdoc />
        public async Task<bool> Exist(int id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }

        /// <inheritdoc />
        public async Task<T> Select(int id)
        {
            return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> Select()
        {
            return await _dataset.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<T> Update(T item)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
            if (result == null)
                return null;
            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
