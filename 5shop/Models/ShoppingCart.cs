using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace _5shop.Models
{
    public class ShoppingCart
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name ="Корисник")]
        public string username { get; set; }
        public List<Product> products { get; set; }
        public List<int> quantities { get; set; }
        public List<int> totalIndividuals { get; set; }
        [Display(Name = "Цена")]

        public int total { get; set; }
        [Display(Name = "Датум на креирање")]

        public DateTime dateCreated { get; set; }
        public ShoppingCartStatus status { get; set; }
        public virtual List<OrdersHistory> orders { get; set; }

        public ShoppingCart()
        {
            products = new List<Product>();
            quantities = new List<int>();
            totalIndividuals = new List<int>();
            orders = new List<OrdersHistory>();
            dateCreated = DateTime.Now;
            status = ShoppingCartStatus.CREATED;
        }
    }
}