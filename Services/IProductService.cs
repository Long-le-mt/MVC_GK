using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCGK.Models;

namespace MVCGK.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product? GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        List<Product> Search(SearchModel searchModel);
        List<Category> GetCategories();

    }
}