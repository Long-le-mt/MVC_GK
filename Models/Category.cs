using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCGK.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}