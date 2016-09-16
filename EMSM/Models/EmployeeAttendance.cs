using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EMS.Models
{
    public class EmployeeAttendance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime curDate { get; set; }

        public int is_Attempt { get; set; }

        [DataType(DataType.Time)]
        public DateTime startTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime endTime { get; set; }

        public DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
        public virtual Employee Employees { get; set; }

    }
}