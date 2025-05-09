using Hakutron1337.Business;
using Hakutron1337.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hakutron1337.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductBusiness _productBusiness;

        public ProductsController(ProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productBusiness.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _productBusiness.GetCategoriesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productBusiness.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _productBusiness.GetCategoriesSelectList(product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productBusiness.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _productBusiness.GetCategoriesSelectList(product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productBusiness.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _productBusiness.GetCategoriesSelectList(product.CategoryId);
            return View(product);
        }
    }
}
