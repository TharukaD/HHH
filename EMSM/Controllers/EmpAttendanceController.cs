using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMSM.Models;
using System.Diagnostics;
using System.Data.Entity;

namespace EMSM.Controllers
{
    public class EmpAttendanceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        public int calculate_num_of_Dates(DateTime date)
        {
            string[] details = date.ToString("d").Split('/');

            int num_of_dates = Int32.Parse(details[1]);
            int num_of_months = Int32.Parse(details[0]);
            int num_of_years = Int32.Parse(details[2]);
            int unique_num = num_of_dates * 30 + num_of_months * 12 + num_of_years;

            return unique_num;
        }

        public ActionResult MarkOlder(int id, int udate)
        {
            EmpAttendance emps = db.EmpAttendances.Single(p => p.ID == id && p.uniqDate == udate);
            emps.is_Attempt = 1;

            string dateTime = "01/08/2008 00:00:00";
            DateTime dtime = Convert.ToDateTime(dateTime);
            emps.startTime = dtime;
            emps.endTime = dtime;

            db.SaveChanges();
            return RedirectToAction("Details");
        }

        public ActionResult UnmarkOlder(int id, int udate)
        {
            EmpAttendance emps = db.EmpAttendances.Single(p => p.ID == id && p.uniqDate == udate);
            emps.is_Attempt = 0;

            string dateTime = "01/08/2008 00:00:00";
            DateTime dtime = Convert.ToDateTime(dateTime);
            emps.startTime = dtime;
            emps.endTime = dtime;

            db.SaveChanges();
            return RedirectToAction("Details");
        }



        // GET: /EmpAttendance/
        public ActionResult Index()
        {
            DateTime dt = DateTime.Now.Date;
            int date_unique_Id = calculate_num_of_Dates(dt);
           // var empattendances = db.EmpAttendances.Include(e => e.Employees);
            var empattendances = from atd in db.EmpAttendances
                                 where atd.uniqDate == date_unique_Id
                                    select atd;
            //var empattendances = db.EmpAttendances.(e => e.uniqDate == date_unique_Id);
           return View(empattendances.ToList());
        }

        // GET: /EmpAttendance/Details/5

        public ActionResult Details(String search_string, int eids = 0)
        {
                ViewBag.todayID = calculate_num_of_Dates(DateTime.Now);
           
                var employee = from emp in db.EmpAttendances select emp;
                /*
                if (!String.IsNullOrEmpty(search_string))
                {
                    employee = employee.Where(s => s.curDate.Equals(search_string));
                }
            */
                if (eids != 0)
                {
                    employee = employee.Where(s => s.ID == eids);
                }
                 
                return View(employee.ToList());
               

        }

        // GET: /EmpAttendance/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Employees, "ID", "Emp_First_Name");
            return View();
        }

        // POST: /EmpAttendance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,uniqDate,curDate,is_Attempt,startTime,endTime")] EmpAttendance empattendance)
        {
            if (ModelState.IsValid)
            {
                db.EmpAttendances.Add(empattendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.Employees, "ID", "Emp_First_Name", empattendance.ID);
            return View(empattendance);
        }

        // GET: /EmpAttendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpAttendance empattendance = db.EmpAttendances.Find(id);
            if (empattendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Employees, "ID", "Emp_First_Name", empattendance.ID);
            return View(empattendance);
        }

        // POST: /EmpAttendance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,uniqDate,curDate,is_Attempt,startTime,endTime")] EmpAttendance empattendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empattendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Employees, "ID", "Emp_First_Name", empattendance.ID);
            return View(empattendance);
        }

        // GET: /EmpAttendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpAttendance empattendance = db.EmpAttendances.Find(id);
            if (empattendance == null)
            {
                return HttpNotFound();
            }
            return View(empattendance);
        }

        // POST: /EmpAttendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpAttendance empattendance = db.EmpAttendances.Find(id);
            db.EmpAttendances.Remove(empattendance);
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

        public ActionResult setTime(int id, int uniqD)
        {
            try
            {
                EmpAttendance emp1 = db.EmpAttendances.Single(p => p.ID == id && p.uniqDate == uniqD);
              
                emp1.endTime = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception)
            {
                Debug.WriteLine("###################################################");
            }
            return RedirectToAction("Index");
        }

        public ActionResult resetTime(int id, int uniqD)
        {
            try
            {
                EmpAttendance emp1 = db.EmpAttendances.Single(p => p.ID == id && p.uniqDate == uniqD);
                string dateTime = "01/08/2008 00:00:00";
                DateTime dtime = Convert.ToDateTime(dateTime);
                emp1.endTime = dtime;
                db.SaveChanges();
            }
            catch (Exception)
            {
                Debug.WriteLine("###################################################");
            }
            return RedirectToAction("Index");
        }
    }
}
