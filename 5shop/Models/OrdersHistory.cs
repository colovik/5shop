using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _5shop.Models
{
    public class OrdersHistory
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public virtual List<ShoppingCart> finishedCarts { get; set; }

        public OrdersHistory()
        {
            finishedCarts = new List<ShoppingCart>();
        }
    }
}