using Hakutron1337.Models;

namespace Hakutron1337.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<List<Category>> GetCategories();

        Task<Product> GetById(int id);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(int id);
    }
}
