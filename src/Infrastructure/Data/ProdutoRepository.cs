using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    /// <inheritdoc />
    public class ProdutoRepository : BaseRepository<ProdutoEntity>, IProdutoRepository
    {
        private DbSet<ProdutoEntity> _dataset;

        public ProdutoRepository(AppDbContext context) : base(context)
        {
            _dataset = _context.Set<ProdutoEntity>();
        }
    }
}
