using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5shop.Models
{
    public class ProductsCategoriesAnimalsViewModel
    {
        public List<Product> products { get; set; }
        public List<Category> categories { get; set; }
        public List<Animal> animals { get; set; }
        public int cartId { get; set; }
    }
}