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
        

        public ProductsCategoriesAnimalsViewModel(string userName)
        {
            // Fetch the user's shopping cart ID
            using (var db = new ApplicationDbContext())
            {
                var cart = db.shoppingCarts.FirstOrDefault(c => c.username == userName) ?? new ShoppingCart();
                cartId = cart.id;
            }

            // Ensure cartId is not 0
            if (cartId == 0)
            {
                Random random = new Random();
                cartId = random.Next(1, 1000);
            }
        }
    }
}