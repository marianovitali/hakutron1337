using Hakutron1337.Models;
using Hakutron1337.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hakutron1337.Business
{
    public class ProductBusiness
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductBusiness(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task AddProduct(Product product)
        {
            await _productRepository.Add(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _productRepository.Update(product);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<SelectList> GetCategoriesSelectList(int? selectedCategoryId = null)
        {
            var categories = await _categoryRepository.GetAllCategories();
            return new SelectList(categories, "Id", "Name", selectedCategoryId);
        }
    }
}
