using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lightway_Academy_school_fee_application.Models;

namespace Lightway_Academy_school_fee_application.Controllers
{
    public class ClassTermsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClassTerms
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.ClassTerms.ToList().OrderBy(t=>t.ClassName));
        }

        // GET: ClassTerms/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTerm classTerm = db.ClassTerms.Find(id);
            if (classTerm == null)
            {
                return HttpNotFound();
            }
            return View(classTerm);
        }

        // GET: ClassTerms/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassTerms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,ClassName")] ClassTerm classTerm)
        {
            if (ModelState.IsValid)
            {
                db.ClassTerms.Add(classTerm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(classTerm);
        }

        // GET: ClassTerms/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTerm classTerm = db.ClassTerms.Find(id);
            if (classTerm == null)
            {
                return HttpNotFound();
            }
            return View(classTerm);
        }

        // POST: ClassTerms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,ClassName")] ClassTerm classTerm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classTerm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classTerm);
        }

        // GET: ClassTerms/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTerm classTerm = db.ClassTerms.Find(id);
            if (classTerm == null)
            {
                return HttpNotFound();
            }
            return View(classTerm);
        }

        // POST: ClassTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassTerm classTerm = db.ClassTerms.Find(id);
            db.ClassTerms.Remove(classTerm);
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
