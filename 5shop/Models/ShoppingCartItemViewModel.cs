using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5shop.Models
{
    public class ShoppingCartItemViewModel
    {
        public string ProductImageUrl { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int SelectedQuantity { get; set; }
        public int TotalItemPrice {  get; set; }
        public int TotalCartPrice {  get; set; }
    }
}
