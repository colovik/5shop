using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _5shop.Models
{
    public class ShoppingCartItemViewModel
    {
            [Key]
            public int Id { get; set; }
            public int productId { get; set; }
            public int shoppingCartId { get; set; }
            public virtual Product product { get; set; }
    }
}
