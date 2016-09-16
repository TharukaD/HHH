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
using System.Diagnostics;
using EMSM.Models;

namespace EmMS.Controllers
{
    public class EmployeeManageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void setCurrentDate()
        {
            DateTime current_Date = DateTime.Now.Date;
            CurrentDateIdentifier curd = db.CurrentDateIdentifiers.Single(p => p.ID == 1);
            DateTime stored_Date = curd.stored_Date;
            
            if (!current_Date.Equals(stored_Date))
            {
                try
                {
                    //Employee emps = db.Employees.Single(p => p.ID == 1);

                    var query = from em in db.Employees
                                select em;
                    foreach (Employee e in query)
                    {
                        e.is_Identify = 0;
                    }

                    curd.stored_Date = current_Date;

                    db.SaveChanges();
                }
                catch (Exception w)
                {
                    Debug.WriteLine("jkjkjkjkj");
                }


            }
        }

        // GET: /EmployeeManageController/
        public ActionResult Index(String search_string, int eids = 0)
        {
            DateTime dt = DateTime.Now.Date;
            ViewBag.curDate = dt.ToString("MM/dd/yyyy");

            setCurrentDate();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var employee = from emp in db.Employees select emp;

            if (!String.IsNullOrEmpty(search_string))
            {
                employee = employee.Where(s => s.Emp_First_Name.Contains(search_string) || s.Emp_Last_Name.Contains(search_string) || s.Emp_NIC.Contains(search_string));
            }

            if (eids != 0)
            {
                employee = employee.Where(s => s.ID == eids);
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            setCurrentDate();

            return View(employee.ToList());

        }

        // GET: /EmployeeManageController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /EmployeeManageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /EmployeeManageController/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Emp_First_Name,Emp_Last_Name,Emp_NIC,Emp_Contact,Emp_Pic_Path,Emp_Money_Per_Hour,Emp_Recomended_Work_Time,is_Identify")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: /EmployeeManageController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /EmployeeManageController/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Emp_First_Name,Emp_Last_Name,Emp_NIC,Emp_Contact,Emp_Pic_Path,Emp_Money_Per_Hour,Emp_Recomended_Work_Time,is_Identify")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: /EmployeeManageController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /EmployeeManageController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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


        /// ///////////////////////////////////////////////////////////////////////////////////////////////
        public int calculate_num_of_Dates(DateTime date)
        {
            string[] details = date.ToString("d").Split('/');

            int num_of_dates = Int32.Parse(details[1]);
            int num_of_months = Int32.Parse(details[0]);
            int num_of_years = Int32.Parse(details[2]);
            int unique_num = num_of_dates * 30 + num_of_months * 12 + num_of_years;

            return unique_num;
        }


        public ActionResult Mark(int id)
        {
            Employee emp = db.Employees.Single(p => p.ID == id);
            emp.is_Identify = 1;                                        // mark attendance in Employee Table
            db.SaveChanges();
            DateTime dt = DateTime.Now.Date;
            int date_unique_Id = calculate_num_of_Dates(dt);

            try
            {
                EmpAttendance emps = db.EmpAttendances.Single(p => p.ID == id && p.uniqDate==date_unique_Id);
                //EmployeeAttendance emps = db.EmployeeAttendances.Single(p => p.ID == id && DbFunctions.TruncateTime(p.curDate).Value ==dt);
                Debug.WriteLine("Employee ID and Current_Date is already inside EmployeeAttendance Table");
                
                try
                {

                    EmpAttendance emp1 = db.EmpAttendances.Single(p => p.ID == id && p.uniqDate == date_unique_Id);
                    //EmployeeAttendance emp1 = db.EmployeeAttendances.Single(p => p.ID == id && DbFunctions.TruncateTime(p.curDate).Value == dt);
                    emp1.is_Attempt = 1;
                    emp1.startTime = DateTime.Now;
                    emp1.endTime = DateTime.Now;
                    db.SaveChanges();
                    
                }
                catch (Exception)
                {
                    Debug.WriteLine("###################################################");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Employee ID is not in EmployeeAttendance Table");

                //EmployeeAttendance ord = new EmployeeAttendance
                EmpAttendance ord = new EmpAttendance
                {
                    ID = id,
                    uniqDate=date_unique_Id,
                    curDate = DateTime.Now.Date,                         //insert details to EmployeeAttendance Table
                    is_Attempt = 1,
                    startTime = DateTime.Now,
                    endTime = DateTime.Now
                };
                db.EmpAttendances.Add(ord);
                //db.EmployeeAttendances.Add(ord);
                db.SaveChanges();

                Debug.WriteLine("Inserted Details to EmployeeAttendance Table");
                Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult UnMark(int id)
        {
            Employee emp = db.Employees.Single(p => p.ID == id);
            emp.is_Identify = 0;                                        // mark attendance in Employee Table
            db.SaveChanges();
            DateTime dt = DateTime.Now.Date;
            int date_unique_Id = calculate_num_of_Dates(dt);

            try
            {
                EmpAttendance emps = db.EmpAttendances.Single(p => p.ID == id && p.uniqDate == date_unique_Id);
                //EmployeeAttendance emps = db.EmployeeAttendances.Single(p => p.ID == id && DbFunctions.TruncateTime(p.curDate).Value == dt);
                Debug.WriteLine("Employee ID is already inside EmployeeAttendance Table");
                try
                {
                    EmpAttendance emp1 = db.EmpAttendances.Single(p => p.ID == id && p.uniqDate == date_unique_Id);
                    //EmployeeAttendance emp1 = db.EmployeeAttendances.Single(p => p.ID == id && DbFunctions.TruncateTime(p.curDate).Value == dt);
                    emp1.is_Attempt = 0;
                    
                    string dateTime = "01/08/2008 00:00:00";
                    DateTime dtime = Convert.ToDateTime(dateTime);
                    emp1.startTime = dtime;
                    emp1.endTime = dtime;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    Debug.WriteLine("###################################################");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Employee ID is not in EmployeeAttendance Table");

                //EmployeeAttendance ord = new EmployeeAttendance
                EmpAttendance ord = new EmpAttendance
                {
                    ID = id,
                    uniqDate = date_unique_Id,
                    curDate = DateTime.Now.Date,                         //insert details to EmployeeAttendance Table
                    is_Attempt = 0,
                    startTime = DateTime.Now,
                    endTime = DateTime.Now
                };
                //db.EmployeeAttendances.Add(ord);
                db.EmpAttendances.Add(ord);
                db.SaveChanges();

                Debug.WriteLine("Inserted Details to EmployeeAttendance Table");

            }

            return RedirectToAction("Index");
        }


    }
}
