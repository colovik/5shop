using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _5shop.Models;
using Microsoft.AspNet.Identity;
using _5shop.Models;

namespace _5shop.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(string categoryFilter, string animalFilter)
        {
            var products = db.products.ToList();
            var categories = (Category[])Enum.GetValues(typeof(Category));
            var animals = (Animal[])Enum.GetValues(typeof(Animal));

            var cartId = 0;

            if (User.Identity.IsAuthenticated)
            {
                var shoppingCart = db.shoppingCarts.FirstOrDefault(sc => sc.username == User.Identity.Name 
                                                                    && sc.status == ShoppingCartStatus.CREATED );
                if (shoppingCart != null)
                {
                    cartId = shoppingCart.id;
                }
            }

            if (!string.IsNullOrEmpty(categoryFilter))
            {
                products = products.Where(p => p.category.ToString() == categoryFilter).ToList();
            }

            if (!string.IsNullOrEmpty(animalFilter))
            {
                products = products.Where(p => p.animal.ToString() == animalFilter).ToList();
            }


            var model = new ProductsCategoriesAnimalsViewModel
            {
                animals = animals.ToList(),
                categories = categories.ToList(),
                products = products,
                cartId = cartId
            };

            return View(model);
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var shoppingCart = db.shoppingCarts.FirstOrDefault(sc => sc.username == User.Identity.Name && sc.status == ShoppingCartStatus.CREATED);
            var shoppingCartId = shoppingCart != null ? shoppingCart.id : 0;


            var model = new DetailsAddToCartViewModel
            {
                product = product,
                cartId = shoppingCartId
            };

            return View(model);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Products/Create
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,quantity,price,description,imageUrl,category,animal")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,quantity,price,description,category,animal")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.products.Find(id);
            db.products.Remove(product);
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
    }
}
