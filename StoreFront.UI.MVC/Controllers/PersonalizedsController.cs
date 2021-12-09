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
    public class PersonalizedsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Personalizeds
        public ActionResult Index()
        {
            return View(db.Personalizeds.ToList());
        }

        // GET: Personalizeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personalized personalized = db.Personalizeds.Find(id);
            if (personalized == null)
            {
                return HttpNotFound();
            }
            return View(personalized);
        }

        // GET: Personalizeds/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personalizeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonalIzedID,PersonalizedType")] Personalized personalized)
        {
            if (ModelState.IsValid)
            {
                db.Personalizeds.Add(personalized);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personalized);
        }

        // GET: Personalizeds/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personalized personalized = db.Personalizeds.Find(id);
            if (personalized == null)
            {
                return HttpNotFound();
            }
            return View(personalized);
        }

        // POST: Personalizeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonalIzedID,PersonalizedType")] Personalized personalized)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalized).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personalized);
        }

        // GET: Personalizeds/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personalized personalized = db.Personalizeds.Find(id);
            if (personalized == null)
            {
                return HttpNotFound();
            }
            return View(personalized);
        }

        // POST: Personalizeds/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personalized personalized = db.Personalizeds.Find(id);
            db.Personalizeds.Remove(personalized);
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
