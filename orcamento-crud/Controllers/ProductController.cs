using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using orcamento_crud.Interface;
using orcamento_crud.Models;

namespace orcamento_crud.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct prod;

        public ProductController(IProduct product)
        {
            prod = product;
        }

        public IActionResult Index()
        {
            List<Product> listProducts = new List<Product>();
            listProducts = prod.GetAllProducts().ToList();
            return View(listProducts);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Product product = prod.GetProduct(id);

            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Product product)
        {
            if(ModelState.IsValid)
            {
                prod.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Product product = prod.GetProduct(id);

            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind] Product product)
        {
            if(id != product.ProductId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                prod.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Product product = prod.GetProduct(id);

            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            prod.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
