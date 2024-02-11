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

            if (shoppingCart == null || !user.Equals(sessionUser))
            {
                shoppingCart = db.shoppingCarts.FirstOrDefault(c => c.status == ShoppingCartStatus.CREATED && c.username == User.Identity.Name);

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

        [HttpPost]
        public ActionResult buy(int cartId, List<int>? products, List<int>? quantities, List<int>? totalIndividuals)
        {
            var shoppingCart = db.shoppingCarts
                .FirstOrDefault(sc => sc.id == cartId);

            if (shoppingCart != null)
            {
                var totalSum = 0;

                for (int i = 0; i < products.Count; i++)
                {
                    var productId = products[i];
                    var product = db.products.Find(productId);

                    if (product != null)
                    {
                        var quantity = quantities[i];
                        var t = totalIndividuals[i];

                        //shoppingCart.products.Add(product);
                        shoppingCart.quantities.Add(quantity);
                        shoppingCart.totalIndividuals.Add(t);

                        product.quantity -= quantity;

                        totalSum += t;
                    }
                }

                shoppingCart.total = totalSum;
                shoppingCart.status = ShoppingCartStatus.FINISHED;
                shoppingCart.dateCreated = DateTime.Now;
                
                db.SaveChanges();
                Session["ShoppingCart"] = null;
            }

            return RedirectToAction("addToCart");
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

        public ActionResult orderHistory()
        {
            var activeUser = User.Identity.Name;
            var userCarts = db.shoppingCarts.Include(sc => sc.products).Where(sc=> sc.username == activeUser && sc.status == ShoppingCartStatus.FINISHED).ToList();

            return View(userCarts);
        }

        public ActionResult moreDetails(int cartId)
        {
            var cart = db.shoppingCarts.Include(sc => sc.products).FirstOrDefault(sc => sc.id == cartId);
            var products = db.shoppingCarts.FirstOrDefault(sc=>sc.id==cartId).products;
            var model = new MoreDetailsViewModel
            {
                shoppingCart = cart,
                products = products
            };
            return View(model);
        }


    }
}

