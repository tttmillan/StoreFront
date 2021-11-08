using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;

namespace StoreFront.Controllers
{
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Color).Include(p => p.Employee).Include(p => p.Personalized).Include(p => p.ProductType);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName");
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName");
            ViewBag.PersonalizedID = new SelectList(db.Personalizeds, "PersonalIzedID", "PersonalizedType");
            ViewBag.TypeID = new SelectList(db.ProductTypes, "TypeID", "TypeName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,BrandID,TypeID,PersonalizedID,IsShipped,EmployeeID,IsOnBackOrder,UnitsSold,Price,Quantity,ColorID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", product.BrandID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", product.ColorID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName", product.EmployeeID);
            ViewBag.PersonalizedID = new SelectList(db.Personalizeds, "PersonalIzedID", "PersonalizedType", product.PersonalizedID);
            ViewBag.TypeID = new SelectList(db.ProductTypes, "TypeID", "TypeName", product.TypeID);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", product.BrandID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", product.ColorID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName", product.EmployeeID);
            ViewBag.PersonalizedID = new SelectList(db.Personalizeds, "PersonalIzedID", "PersonalizedType", product.PersonalizedID);
            ViewBag.TypeID = new SelectList(db.ProductTypes, "TypeID", "TypeName", product.TypeID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,BrandID,TypeID,PersonalizedID,IsShipped,EmployeeID,IsOnBackOrder,UnitsSold,Price,Quantity,ColorID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "BrandName", product.BrandID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", product.ColorID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FirstName", product.EmployeeID);
            ViewBag.PersonalizedID = new SelectList(db.Personalizeds, "PersonalIzedID", "PersonalizedType", product.PersonalizedID);
            ViewBag.TypeID = new SelectList(db.ProductTypes, "TypeID", "TypeName", product.TypeID);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
