using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCGK.Models;
using MVCGK.Services;

namespace MVCGK.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService _productService;
        public ProductController(IProductService productService, ILogger<HomeController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            // var products = new List<Product>();
            // var products = this._productService.GetProducts();
            var categories = this._productService.GetCategories();
            ViewBag.Categories = categories;


            var categoryId = HttpContext.Request?.Query["categoryId"];
            var key = HttpContext.Request?.Query["keyword"];

            List<Product> products = new List<Product>();
            if (String.IsNullOrEmpty(categoryId) || String.IsNullOrEmpty(key))
            {
                products = this._productService.GetProducts();
                return View(products);
            }
            else
            {
                products = this._productService.Search(new SearchModel { Keyword = key, CategoryId = int.Parse(categoryId.ToString()) });
                return View(products);
            }
        }
        public IActionResult Create()
        {
            var categories = this._productService.GetCategories();
            return View(categories);
        }

        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return RedirectToAction("Create");

            var categories = _productService.GetCategories();
            ViewBag.Product = product;
            return View(categories);
        }

        public IActionResult Search(SearchModel searchModel)
        {
            // List<Product> products = new List<Product>();
            // products = this._productService.Search(searchModel);
            // var products = this._productService.Search(searchModel);
            // TempData["data"] = products;
            return RedirectToAction("Index", new { keyword = searchModel.Keyword, categoryId = searchModel.CategoryId });
        }


        public IActionResult Save(Product product)
        {
            if (product.Id == 0)
            {
                _productService.CreateProduct(product);
            }
            else
            {
                _productService.UpdateProduct(product);
            }
            return RedirectToAction("Index");
        }
    }
}