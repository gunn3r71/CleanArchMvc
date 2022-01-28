using System.Threading.Tasks;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces.Repositories;
using CleanArchMvc.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                                    .Include(x => x.Category)
                                    .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}