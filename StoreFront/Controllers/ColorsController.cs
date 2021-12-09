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
    public class ColorsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Colors
        public ActionResult Index()
        {
            return View(db.Colors.ToList());
        }

        #region Original Scaffolded Code      
        //// GET: Colors/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Color color = db.Colors.Find(id);
        //    if (color == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(color);
        //}

        //// GET: Colors/Create
        //[Authorize(Roles = "Admin")]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Colors/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ColorID,ColorName")] Color color)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Colors.Add(color);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(color);
        //}

        //// GET: Colors/Edit/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Color color = db.Colors.Find(id);
        //    if (color == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(color);
        //}

        //// POST: Colors/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ColorID,ColorName")] Color color)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(color).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(color);
        //}

        //// GET: Colors/Delete/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Color color = db.Colors.Find(id);
        //    if (color == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(color);
        //}

        //// POST: Colors/Delete/5
        //[Authorize(Roles = "Admin")]
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Color color = db.Colors.Find(id);
        //    db.Colors.Remove(color);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        #endregion

        //----------AJAX - Async JavaScript and Xml--------------//
        //All methods/action below were built from scratch to allow for asynchronous functionality

        //Delete - delete a publisher record, returns only json data on id and confirmation
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDelete(int id)
        {
            //Find the pub by id
            Color color = db.Colors.Find(id);
            db.Colors.Remove(color);
            db.SaveChanges();

            string confirmMessage = string.Format("Delete color '{0}' from the database!", color.ColorName);
            return Json(new { id = id, message = confirmMessage });
        }

        [HttpGet]
        public PartialViewResult ColorDetails(int id)
        {
            Color color = db.Colors.Find(id);
            return PartialView(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxCreate(Color color)
        {
            db.Colors.Add(color);
            db.SaveChanges();
            return Json(color);

            //Create a partial view, template: Publisher, data context: BookStorePlusEntities, check partial view.
        }

        //Get the edit partial view
        [HttpGet]
        public PartialViewResult ColorEdit(int id)
        {
            Color color = db.Colors.Find(id);
            return PartialView(color);

            //Create a partial view: Template - Edit for PUblisher, data context class will be BookStorePlusEntities, check the partial view option
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxEdit(Color color)
        {
            db.Entry(color).State = EntityState.Modified;
            db.SaveChanges();
            return Json(color);
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
