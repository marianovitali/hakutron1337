using Hakutron1337.Models;
using Microsoft.EntityFrameworkCore;

namespace Hakutron1337.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _context.Products
                .Include(x => x.Category)
                .ToListAsync();

            return products;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        public async Task Update(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        Task<List<Category>> IProductRepository.GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
