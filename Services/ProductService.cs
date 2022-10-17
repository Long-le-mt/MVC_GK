using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCGK.Models;

namespace MVCGK.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            this._context.Products.Add(product);
            this._context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var existedProduct = this.GetProductById(id);
            if (existedProduct == null) return;
            _context.Products.Remove(existedProduct);
            _context.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return this._context.Categories.ToList();
        }

        public Product? GetProductById(int id)
        {
            return this._context.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts()
        {
            return this._context.Products.Include(p => p.Category).ToList();
        }

        public void UpdateProduct(Product product)
        {
            var existedProduct = this.GetProductById(product.Id);
            if (existedProduct == null) return;
            existedProduct.Name = product.Name;
            existedProduct.Price = product.Price;
            existedProduct.Quantity = product.Quantity;
            existedProduct.CategoryId = product.CategoryId;
            _context.Products.Update(existedProduct);
            _context.SaveChanges();
        }

        public List<Product> Search(SearchModel searchModel)
        {
            return this._context.Products
                    .Where(p => p.Name.ToUpper().Trim().Contains(searchModel.Keyword.Trim().ToUpper())
                    && p.CategoryId == searchModel.CategoryId).ToList();
        }
    }
}