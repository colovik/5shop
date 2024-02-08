using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _5shop.Models;

namespace _5shop.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingCarts
        public ActionResult Index()
        {
            return View(db.shoppingCarts.ToList());
        }

        // GET: ShoppingCarts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.shoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,total,dateCreated")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                db.shoppingCarts.Add(shoppingCart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.shoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,total,dateCreated")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingCart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shoppingCart = db.shoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingCart shoppingCart = db.shoppingCarts.Find(id);
            db.shoppingCarts.Remove(shoppingCart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult addToCart(int? cartId, string? productName)
        {
            var shoppingCart = Session["ShoppingCart"] as ShoppingCart;
            var sessionUser = Session["User"];
            var user = User.Identity.Name;

            // If the session shopping cart is not available, check the database
            if (shoppingCart == null || !user.Equals(sessionUser))
            {
                // Retrieve the current shopping cart from the database based on the user
                shoppingCart = db.shoppingCarts.FirstOrDefault(c => //c.id == cartId &&
                                                                     c.status == ShoppingCartStatus.CREATED &&
                                                                     c.username == User.Identity.Name);

                // If the shopping cart doesn't exist in the database, create a new one
                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCart
                    {
                        username = User.Identity.Name,
                    };

                    db.shoppingCarts.Add(shoppingCart);
                    db.SaveChanges();
                }

                Session["ShoppingCart"] = shoppingCart;
            }

            if (productName != null)
            {
                productName = productName.Replace("\n", "").Trim();
                var product = db.products.FirstOrDefault(z => z.name == productName);

                if (product != null)
                {
                    shoppingCart.products.Add(product);
                    db.SaveChanges();
                }
            }
            Session["User"] = user;

            return View(shoppingCart);
        }

        //public ActionResult buy(int cartId)
        //{
        //    return RedirectToAction("buy");
        //}

        [HttpPost]
        public ActionResult buy(int cartId, List<int> products, List<int> quantities, List<int> totalIndividuals)
        {
            // Fetch the shopping cart with eager loading
            var shoppingCart = db.shoppingCarts
                //.Include(sc => sc.products)
                .FirstOrDefault(sc => sc.id == cartId);

            var totalSum = 0;

            for (int i = 0; i < products.Count; i++)
            {
                var productId = products[i];
                var product = db.products.Find(productId);

                if (product != null)
                {
                    var quantity = quantities[i];
                    var t = totalIndividuals[i];

                    //gi zacuvuvam vo kosnickata (istorija) pred da ja namalam vrednosta vo baza
                    shoppingCart.products.Add(product);
                    shoppingCart.quantities.Add(quantity);
                    shoppingCart.totalIndividuals.Add(t);

                    product.quantity -= quantity;
                    totalSum += t;
                }
            }

            // Update shopping cart totals and status
            shoppingCart.total = totalSum;
            shoppingCart.status = ShoppingCartStatus.FINISHED;

            // Save changes to the database
            db.SaveChanges();

            return RedirectToAction("orderConfirmation");
        }

        public ActionResult orderConfirmation()
        {
            Debug.WriteLine("Entering orderConfirmation action.");

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            return View();
        }

        [HttpPost]
        public ActionResult removeProduct(int productId)
        {
            var shoppingCart = Session["ShoppingCart"] as ShoppingCart;

            if (shoppingCart != null)
            {
                var idProductToRemove = shoppingCart.products.FirstOrDefault(p => p.id == productId);

                if (idProductToRemove != null)
                {
                    shoppingCart.products.Remove(idProductToRemove);
                    Session["ShoppingCart"] = shoppingCart;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("addToCart");
        }


    }
}

