using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace _5shop.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Display(Name="Производ")]
        public String name { get; set; }
        [Required]
        [Display(Name = "Количина")]
        public int quantity {  get; set; }
        [Required]
        [Display(Name = "Цена")]
        public int price { get; set; }
        [Required]
        [Display(Name = "Опис")]
        public string description { get; set; }
        [Required]
        [Display(Name = "Слика")]
        public String imageUrl { get; set; }
        [Required]
        [Display(Name = "Категорија")]
        public virtual Category? category { get; set; }
        [Required]
        [Display(Name = "Животно")]
        public virtual Animal? animal { get; set; }
    }
}