using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Models;


namespace EMSM.Models
{
    public class EmpAttendance
    {
        [Key]
        [Column(Order=0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int uniqDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime curDate { get; set; }

        public int is_Attempt { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime startTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime endTime { get; set; }

        public DbSet<EmpAttendance> EmpAttendances { get; set; }
        public virtual Employee Employees { get; set; }
    }
}