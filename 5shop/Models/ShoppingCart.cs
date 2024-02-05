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
        public string username { get; set; }
        [Required]
        public List<Product> products { get; set; }
        [Required]
        public List<int> prices { get; set; } 
        [Required]
        public List<int> quantities { get; set; } 
        [Required]
        public List<int> totalIndividuals { get; set; }
        [Required]
        public int total { get; set; }
        [Required]
        public DateTime dateCreated { get; set; }

        public ShoppingCart()
        {
            products = new List<Product>();
            prices = new List<int>();
            quantities = new List<int>();
            totalIndividuals = new List<int>();
            dateCreated = DateTime.Now;
        }
    }
}