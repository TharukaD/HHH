using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace EMSM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Debug.WriteLine(calculate_num_of_Dates(DateTime.Now));
            
            return View();
        }

        private void calculate_num_of_Dates(int p1, int p2, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public int calculate_num_of_Dates(DateTime date)
        {
            string[] details = date.ToString("d").Split('/');
           
            int num_of_dates = Int32.Parse(details[1]);
            int num_of_months = Int32.Parse( details[0]);
            int num_of_years = Int32.Parse(details[2]);
            int unique_num = num_of_dates * 30 + num_of_months * 12 + num_of_years;

            return unique_num;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}