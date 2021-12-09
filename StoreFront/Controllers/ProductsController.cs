using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
//using StoreFront.UI.MVC.Utilities;
using System.Drawing;
using StoreFront.Models;


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

        public ActionResult ProductsGrid()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Color).Include(p => p.Employee).Include(p => p.Personalized).Include(p => p.ProductType);

            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Unresolved", "Errors");
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        #region Custom Add-to-Cart Functionality
        public ActionResult AddToCart(int qty, int productID)
        {
            //Create an empty shell for LOCAL shopping cart variable
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //session-based variables are used to manage the state of functionality in our application. In this case, we will store/use the information from a session based variable called cart. Below, we are checking to see if session["cart"] exists... if so, use it to populate the shoppingCart (local variable)
            if (Session["cart"] != null)
            {
                //Session{"cart"] exists and we will put its items in the local shoppingCart

                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
                //With Session variables, think about the process of BOXING and UNBOXING from ArrayLists. Session stores the data in an object format/datatype, so here we must UNBOX that information to its original datatype so we can use it in the logic in our action.
            }

            else
            {
                //Shopping cart doesn't exist, we need to instantiate/create it.
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }//After this if/else, we now have an accurate picture of the shoppingCart and are ready to add things to it.

            //find the product in the db by its id
            Product product = db.Products.Where(b => b.ProductID == productID).FirstOrDefault();

            if (product == null)
            {
                //if bad ID, kick them back to the main view of products
                return RedirectToAction("Index");
            }
            else
            {
                //if books is valid, add the line-item to the cart
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //put the item into the cart BUT if we already have the product as a cart-itm, then update the qty instead. This is why we have the dictionary.
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;//Here we access the quantity for that line-item and add the number of items they selected.
                }
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }

                //We have updated our local objects, and now we need to update the Session ["cart"]
                Session["cart"] = shoppingCart;//NO EXPLICIT CASTING needed to add this item to the shoppingCart as the shoppingCart is being implicitly cast into a larger container.
            }

            //send the user to the shoppingcart view to see the cart items
            return RedirectToAction("Index", "ShoppingCart");
        }
        #endregion

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            
            ViewBag.Image = new SelectList(db.Products, "ProductImage", "ProductName");
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
        public ActionResult Create([Bind(Include = "ProductID,Name,BrandID,TypeID,PersonalizedID,IsShipped,EmployeeID,IsOnBackOrder,UnitsSold,Price,Quantity,ColorID,ProductImage,Description")] Product product)
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
        public ActionResult Edit([Bind(Include = "ProductID,Name,BrandID,TypeID,PersonalizedID,IsShipped,EmployeeID,IsOnBackOrder,UnitsSold,Price,Quantity,ColorID,ProductImage,Description")] Product product)
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
