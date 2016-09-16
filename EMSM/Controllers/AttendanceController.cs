using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMS.Models;
using EmMS.Models;

namespace EmMS.Controllers
{
    public class AttendanceController : Controller
    {
        private EMSM.Models.ApplicationDbContext db = new EMSM.Models.ApplicationDbContext();

        // GET: /AttendanceController/
        public ActionResult Index()
        {
            return View(db.EmployeeAttendances.ToList());
        }

        // GET: /AttendanceController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAttendance employeeattendance = db.EmployeeAttendances.Find(id);
            if (employeeattendance == null)
            {
                return HttpNotFound();
            }
            return View(employeeattendance);
        }
        /*
        public ActionResult Details(int? id, DateTime dt)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAttendance employeeattendance = db.EmployeeAttendances.Single(c => c.ID == id && c.curDate == dt);
            if (employeeattendance == null)
            {
                return HttpNotFound();
            }
            return View(employeeattendance);
        }
        */


        // GET: /AttendanceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AttendanceController/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,curDate,is_Attempt,startTime,endTime")] EmployeeAttendance employeeattendance)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeAttendances.Add(employeeattendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeattendance);
        }

        // GET: /AttendanceController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAttendance employeeattendance = db.EmployeeAttendances.Find(id);
            if (employeeattendance == null)
            {
                return HttpNotFound();
            }
            return View(employeeattendance);
        }

        // POST: /AttendanceController/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,curDate,is_Attempt,startTime,endTime")] EmployeeAttendance employeeattendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeattendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeattendance);
        }

        // GET: /AttendanceController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAttendance employeeattendance = db.EmployeeAttendances.Find(id);
            if (employeeattendance == null)
            {
                return HttpNotFound();
            }
            return View(employeeattendance);
        }

        // POST: /AttendanceController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeAttendance employeeattendance = db.EmployeeAttendances.Find(id);
            db.EmployeeAttendances.Remove(employeeattendance);
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
