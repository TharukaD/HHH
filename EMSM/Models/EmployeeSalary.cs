using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity;

namespace EMS.Models
{
    public class EmployeeSalary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [C
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        public DateTime Year_and_Month { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public int Short_Leaves { get; set; }
        public int Holidays { get; set; }

        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public virtual Employee Employee { get; set; }
    }
}