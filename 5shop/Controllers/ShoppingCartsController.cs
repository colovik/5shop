using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

        //public ActionResult addToCart(int cartId, int productId/*, int quantity*/)
        //{
        //    // kosnicka na korisnikot sho e najaven ili prazna kosnicka
        //    var cart = db.shoppingCarts.Find(cartId) ?? new ShoppingCart();

        //    //go naogja proizvodot kaj sho sum kliknala po id sho go prakjam so javascript
        //    var product = db.products.FirstOrDefault(p => p.id == productId);

        //    //vo kolickata go dodavam proizvodot
        //    cart.products.Add(product);
        //    cart.prices.Add(product.price);
        //    //cart.quantities.Add(quantity);
        //    //cart.totalIndividuals.Add(quantity * product.price);
        //    //cart.total += quantity * product.price;

        //    ShoppingCartItemViewModel model = new ShoppingCartItemViewModel();
        //    model.ProductPrice = product.price;
        //    model.ProductName = product.name;
        //    model.ProductImageUrl = product.imageUrl;
        //    //model.SelectedQuantity = quantity;
        //    //model.TotalItemPrice = quantity * product.price;
        //    //model.TotalCartPrice = cart.total + (quantity * product.price);

        //    // Redirect to the ShoppingCart view or action
        //    return View(model);
        //}

        //public ActionResult getShoppingCart(ShoppingCartItemViewModel model)
        //{
        //    return View(model);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

