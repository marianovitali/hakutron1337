using Hakutron1337.Business;
using Hakutron1337.Models;
using Hakutron1337.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productBusiness.GetProductById(id);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }                                                                                                                                                         
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _productBusiness.GetAllCategories();
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productBusiness.Add(product);
                return RedirectToAction(nameof(Index));

            }
            // Si el modelo no es válido, volvemos a cargar la lista de categorías
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

            // Si el modelo no es válido, volvemos a cargar la lista de categorías

            ViewBag.Categories = await _productBusiness.GetCategoriesSelectList(product.CategoryId);
            return View(product);

        }



        // POST: Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productBusiness.GetProductById(id);


            await _productBusiness.Delete(id);

            return RedirectToAction(nameof(Index));
        }


        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}

    }
}
    