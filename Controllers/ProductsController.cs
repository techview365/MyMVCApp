using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class ProductsController : Controller
    {
        private static List<Products> _products = new List<Products>()
        {
            new Products{Id = 1, Name = "Product1", Price = 10.0m},
            new Products{Id = 2, Name = "Product2", Price = 20.0m}
        };
        public IActionResult Index()
        {
            return View(_products);
        }
        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Products product)
        {
            if (ModelState.IsValid)
            {
                product.Id = _products.Max(p => p.Id) + 1;
                _products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Products product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == id);
                if(existingProduct == null)
                {
                    return NotFound();
                }
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _products.Remove(product);
            return RedirectToAction(nameof(Index));

        }
    }
}
