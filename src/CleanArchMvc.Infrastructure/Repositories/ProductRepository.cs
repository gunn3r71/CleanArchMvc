using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces.Repositories;
using CleanArchMvc.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleanArchMvc.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Product> GetProductCategoryAsync(int Id)
        {
            return await _context.Products
                                    .Include(x => x.Category)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}