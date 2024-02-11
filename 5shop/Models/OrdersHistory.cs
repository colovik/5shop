using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5shop.Models
{
    public class OrdersHistory
    {
        public string username { get; set; }
        public List<ShoppingCart> orders { get; set; }
    }
}