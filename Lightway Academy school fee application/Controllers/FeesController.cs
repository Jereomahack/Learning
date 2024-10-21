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
    public class FeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fees
        public ActionResult Index(string SearchString)
        {
            //getting session from settings
            Setting getsession = db.Settings.FirstOrDefault(t => t.Name == "Session");

            if (SearchString == null)
            {
                return View(db.Fees.ToList().OrderBy(t=>t.ClassName).Where(t => t.Session == getsession.Value));
            }
            else if (SearchString != "")
            {
                var Fees = from r in db.Fees
                           where r.Session.Contains(SearchString.Trim())
                           select r;
                return View(Fees.ToList().OrderBy(t => t.ClassName));
            }

            return View(db.Fees.ToList().OrderBy(t=>t.ClassName).Where(t => t.Session == getsession.Value));
        }

        // GET: Fees/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fees fees = db.Fees.Find(id);
            if (fees == null)
            {
                return HttpNotFound();
            }
            return View(fees);
        }

        // GET: Fees/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ClassName = new SelectList(db.ClassTerms.OrderBy(r => r.ClassName), "ClassName", "ClassName");

            return View();
        }

        // POST: Fees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,ClassName,Session,Term,Amount")] Fees fees)
        {
            ViewBag.ClassName = new SelectList(db.ClassTerms.OrderBy(r => r.ClassName), "ClassName", "ClassName");
            if (ModelState.IsValid)
            {
                db.Fees.Add(fees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fees);
        }

        // GET: Fees/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fees fees = db.Fees.Find(id);
            if (fees == null)
            {
                return HttpNotFound();
            }
            return View(fees);
        }

        // POST: Fees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,ClassName,Session,Term,Amount")] Fees fees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fees);
        }

        // GET: Fees/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fees fees = db.Fees.Find(id);
            if (fees == null)
            {
                return HttpNotFound();
            }
            return View(fees);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Fees fees = db.Fees.Find(id);
            db.Fees.Remove(fees);
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
