using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5shop.Models
{
    public class MoreDetailsViewModel
    {
        public ShoppingCart shoppingCart { get; set; }
        public List<Product> products { get; set; }
    }
}