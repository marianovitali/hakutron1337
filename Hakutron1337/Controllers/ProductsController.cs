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

        private readonly IProductRepository _productRepository;


        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProducts();

            return View(products);
        }

        //public IActionResult Index2()
        //{
        //    return View();
        //}

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }                                                                                                                                                         
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Products.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        //    return View(product);
        //}

        public async Task<IActionResult> Create(Product product)
        {

            await _productRepository.Add(product);

            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

        //    return View(product);
        //}

        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            await _productRepository.Update(product);
            return RedirectToAction(nameof(Index));
        }



        // POST: Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstAsync(x => x.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    }
}
    