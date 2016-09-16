using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace EMS.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [StringLength(10, ErrorMessage = "Enter the First Name that not exeed 10 characters.")]
        public string Emp_First_Name { get; set; }

        [StringLength(10, ErrorMessage = "Enter the Last Name that not exeed 10 characters.")]
        public string Emp_Last_Name { get; set; }

        [Required]
        public string Emp_NIC { get; set; }

        [Required]
        public string Emp_Contact { get; set; }
        public string Emp_Pic_Path { get; set; }

        [DataType(DataType.Currency)]
        public decimal Emp_Money_Per_Hour { get; set; }

        public int Emp_Recomended_Work_Time { get; set; }

        public int is_Identify { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public virtual ICollection<EmployeeSalary> EmployeeSalarys { get; set; }
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
    }
}