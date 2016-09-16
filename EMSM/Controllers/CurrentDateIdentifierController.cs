using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmMS.Models;
using EMSM.Models;

namespace EMSM.Controllers
{
    public class CurrentDateIdentifierController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /CurrentDateIdentifier/
        public ActionResult Index()
        {
            return View(db.CurrentDateIdentifiers.ToList());
        }

        // GET: /CurrentDateIdentifier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentDateIdentifier currentdateidentifier = db.CurrentDateIdentifiers.Find(id);
            if (currentdateidentifier == null)
            {
                return HttpNotFound();
            }
            return View(currentdateidentifier);
        }

        // GET: /CurrentDateIdentifier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CurrentDateIdentifier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,stored_Date")] CurrentDateIdentifier currentdateidentifier)
        {
            if (ModelState.IsValid)
            {
                db.CurrentDateIdentifiers.Add(currentdateidentifier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currentdateidentifier);
        }

        // GET: /CurrentDateIdentifier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentDateIdentifier currentdateidentifier = db.CurrentDateIdentifiers.Find(id);
            if (currentdateidentifier == null)
            {
                return HttpNotFound();
            }
            return View(currentdateidentifier);
        }

        // POST: /CurrentDateIdentifier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,stored_Date")] CurrentDateIdentifier currentdateidentifier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currentdateidentifier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currentdateidentifier);
        }

        // GET: /CurrentDateIdentifier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentDateIdentifier currentdateidentifier = db.CurrentDateIdentifiers.Find(id);
            if (currentdateidentifier == null)
            {
                return HttpNotFound();
            }
            return View(currentdateidentifier);
        }

        // POST: /CurrentDateIdentifier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrentDateIdentifier currentdateidentifier = db.CurrentDateIdentifiers.Find(id);
            db.CurrentDateIdentifiers.Remove(currentdateidentifier);
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
